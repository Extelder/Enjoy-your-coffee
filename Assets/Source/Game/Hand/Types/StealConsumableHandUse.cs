using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealConsumableHandUse : HandUseConsumable
{
    private Hand _hand;
    public static event Action CanSteel;

    public override void Use(Hand hand)
    {
        _hand = hand;
    }

    public override void UseOnAnimation()
    {
        CanSteel?.Invoke();
        _hand.CanSteal = true;
    }
}