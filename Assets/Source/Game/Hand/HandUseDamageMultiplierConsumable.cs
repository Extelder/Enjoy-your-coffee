using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandUseDamageMultiplierConsumable : HandUseConsumable
{
    public override void Use(Hand hand)
    {
        GameState.Instance.Coffee.DamageCharacteristics.ChangeValue(GameState.Instance.Coffee.DamageCharacteristics
            .Value * 2);
    }
}