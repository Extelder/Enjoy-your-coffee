using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CoffeeDrinker : MonoBehaviour
{
    [SerializeField] private Transform _drinkPoint;
    [SerializeField] private float _coffeeMoveSpeed = 1f;
    [SerializeField] private Ease _ease = Ease.InOutQuint;

    public event Action<int> CoffeeDrinked;

    private Sequence _drinkSequence;

    private void OnEnable()
    {
        if (GameState.Instance != null)
            GameState.Instance.GameRestart += OnGameRestart;
    }

    private void OnGameRestart()
    {
        _drinkSequence?.Kill();
        _drinkSequence = null;
    }

    private void OnDisable()
    {
        if (GameState.Instance != null)
            GameState.Instance.GameRestart -= OnGameRestart;

        _drinkSequence?.Kill();
        _drinkSequence = null;
    }

    public void Drink(Coffee coffee)
    {
        if (coffee == null && _drinkPoint == null)
            return;

        _drinkSequence?.Kill();

        _drinkSequence = DOTween.Sequence();
        _drinkSequence.Append(coffee.transform.DOMove(_drinkPoint.position, _coffeeMoveSpeed).SetEase(_ease));
        _drinkSequence.Join(coffee.transform.DORotate(_drinkPoint.eulerAngles, _coffeeMoveSpeed).SetEase(_ease));
        _drinkSequence.OnComplete(() =>
        {
            coffee.Drink(() =>
            {
                if (!this || !GameState.Instance)
                    return;

                int damageValue = coffee.DamageCharacteristics.Value;
                CoffeeDrinked?.Invoke(damageValue);

                // Проверяем, будет ли рестарт
                bool didRestart = GameState.Instance.CoffeeSwitcher.NextCoffee();

                // ИСПРАВЛЕНИЕ: Если был рестарт - НЕ меняем руку!
                if (!didRestart)
                {
                    if (!(damageValue == 0 && GameState.Instance.CurrentHand != null &&
                          GameState.Instance.CurrentHand.CoffeeDrinker == this))
                        GameState.Instance.NextHand();
                }

                // Деактивируем ТОЛЬКО если не было рестарта
                if (!didRestart)
                    coffee.gameObject.SetActive(false);
                // Деактивируем ТОЛЬКО если не было рестарта
                if (!didRestart)
                    coffee.gameObject.SetActive(false);
            });
        });
    }
}