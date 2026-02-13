using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeDamager : MonoBehaviour
{
    [SerializeField] private CoffeeDrinker _coffeeDrinker;
    
    private void OnEnable()
    {
        _coffeeDrinker.CoffeeDrinked += OnCoffeeDrinked;
    }

    private void OnCoffeeDrinked(int value)
    {
        PlayerHealth.Instance.TakeDamage(value);
    }

    private void OnDisable()
    {
        _coffeeDrinker.CoffeeDrinked -= OnCoffeeDrinked;
    }
}
