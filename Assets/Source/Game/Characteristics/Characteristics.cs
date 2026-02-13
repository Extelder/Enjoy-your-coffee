using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Characteristics<T> : MonoBehaviour
{
    [field: SerializeField] public T StartValue { get; private set; }
    public abstract T Value { get; set; }
    public abstract event Action<T> ValueChanged;

    private void Awake()
    {
        Value = StartValue;
    }

    public abstract void ChangeValue(T value);
}
