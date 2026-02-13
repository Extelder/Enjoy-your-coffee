using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandUsePoisonChecker : HandUseConsumable
{
    public override void Use(Hand hand)
    {
        if (GameState.Instance.Coffee.DamageCharacteristics.Value == 0)
        {
            Debug.Log("NO DAMAGE");
            return;
        }
        GameState.Instance.Coffee.ShowIndicator();
        Debug.Log("DOHUYA DAMAGE");
    }
}