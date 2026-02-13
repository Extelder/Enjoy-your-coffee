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

    public void Drink(Coffee coffee)
    {
        coffee.transform.DOMove(_drinkPoint.position, _coffeeMoveSpeed).SetEase(_ease);
        coffee.transform.DORotate(_drinkPoint.eulerAngles, _coffeeMoveSpeed).SetEase(_ease).OnComplete(() =>
        {
            coffee.Drink(() =>
            {
                CoffeeDrinked?.Invoke(coffee.DamageCharacteristics.Value);
                GameState.Instance.CoffeeSwitcher.NextCoffee();
                GameState.Instance.NextHand();
                Destroy(coffee.gameObject);
            });
        });
    }
}