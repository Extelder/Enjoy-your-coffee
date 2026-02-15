using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CoffeeDamageCharacteristics : Characteristics<int>
{
    public override int Value { get; set; }
    public override event Action<int> ValueChanged;

    private void OnEnable()
    {
        ChangeValue(Random.Range(0, 2));
    }

    public override void ChangeValue(int value)
    {
        Value = value;
        ValueChanged?.Invoke(Value);
    }
}