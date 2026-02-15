using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandUsePoisonChecker : HandUseConsumable
{
    public override void Use(Hand hand)
    {
    }

    public override void UseOnAnimation()
    {
        if (GameState.Instance.Coffee.DamageCharacteristics.Value == 0)
        {
            return;
        }
        GameState.Instance.Coffee.ShowIndicator();
    }
}