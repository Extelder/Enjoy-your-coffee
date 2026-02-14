using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealConsumableHandUse : HandUseConsumable
{
    public static event Action CanSteel;

    public override void Use(Hand hand)
    {
        CanSteel?.Invoke();
        hand.CanSteal = true;
    }
}