using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandUseCoffeeDestroyer : HandUseConsumable
{
    public override void Use(Hand hand)
    {
        if (GameState.Instance.Coffee.DamageCharacteristics.Value == 0)
        {
            return;
        }
        GameState.Instance.Coffee.DestroyCup();
    }
}
