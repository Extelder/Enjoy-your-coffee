using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CoffeeDrinker : MonoBehaviour
{
    [SerializeField] private Transform _drinkPoint;
    [SerializeField] private float _coffeeMoveSpeed;

    [SerializeField] private Ease _ease = Ease.InOutQuint;

    public event Action<int> CoffeeDrinked;

    private Tween _tween;
    private Tween _tween1;

    private void OnEnable()
    {
        GameState.Instance.GameRestart += OnGameRestart;
    }

    private void OnGameRestart()
    {
        _tween?.Kill();
        _tween1?.Kill();
    }

    private void OnDisable()
    {
        GameState.Instance.GameRestart -= OnGameRestart;
        _tween?.Kill();
        _tween1?.Kill();
    }

    public void Drink(Coffee coffee)
    {
        _tween = coffee.transform.DOMove(_drinkPoint.position, _coffeeMoveSpeed).SetEase(_ease);
        _tween1 = coffee.transform.DORotate(_drinkPoint.eulerAngles, _coffeeMoveSpeed).SetEase(_ease).OnComplete(() =>
        {
            coffee.Drink(() =>
            {
                CoffeeDrinked?.Invoke(coffee.DamageCharacteristics.Value);
                GameState.Instance.CoffeeSwitcher.NextCoffee();
                if (!(coffee.DamageCharacteristics.Value == 0 && GameState.Instance.CurrentHand.CoffeeDrinker == this))
                    GameState.Instance.NextHand();
                coffee.gameObject.SetActive(false);
            });
        });
    }
}