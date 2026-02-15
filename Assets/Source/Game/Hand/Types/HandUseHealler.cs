using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandUseHealler : HandUseConsumable
{
    [SerializeField] private int _amountToHeal;
    [SerializeField] private Health _health;

    public override void Use(Hand hand)
    {
    }

    public override void UseOnAnimation()
    {
        _health.Heal(_amountToHeal);
    }
}