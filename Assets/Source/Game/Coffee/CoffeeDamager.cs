using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeDamager : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private CoffeeDrinker _coffeeDrinker;

    private void OnEnable()
    {
        _coffeeDrinker.CoffeeDrinked += OnCoffeeDrinked;
    }

    private void OnCoffeeDrinked(int value)
    {
        _health.TakeDamage(value);
    }

    private void OnDisable()
    {
        _coffeeDrinker.CoffeeDrinked -= OnCoffeeDrinked;
    }
}