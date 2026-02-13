using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeDamageCharacteristics : Characteristics<int>
{
    public override int Value { get; set; }
    public override event Action<int> ValueChanged;
    
    public override void ChangeValue(int value)
    {
        Value = value;
        ValueChanged?.Invoke(Value);
    }
}
