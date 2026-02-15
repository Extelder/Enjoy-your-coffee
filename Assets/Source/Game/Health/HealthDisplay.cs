using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthDisplay : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private GameObject[] _healthDisplays;

    private void OnEnable()
    {
        _health.HealthValueChanged += OnHealthValueChanged;
    }

    private void OnHealthValueChanged(int value)
    {
        for (int i = 0; i < _healthDisplays.Length; i++)
        {
            _healthDisplays[i].SetActive(false);
        }
        for (int i = 0; i < value; i++)
        {
            _healthDisplays[i].SetActive(true);
        }
    }

    private void OnDisable()
    {
        _health.HealthValueChanged -= OnHealthValueChanged;
    }
}
