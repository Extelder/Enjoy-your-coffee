using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeInverterHandUse : HandUseConsumable
{
    public override void Use(Hand hand)
    {
        if (GameState.Instance.Coffee.DamageCharacteristics.Value < 1)
        {
            GameState.Instance.Coffee.DamageCharacteristics.Value = 1;
        }
        else
        {
            GameState.Instance.Coffee.DamageCharacteristics.Value = 0;
        }
    }
}