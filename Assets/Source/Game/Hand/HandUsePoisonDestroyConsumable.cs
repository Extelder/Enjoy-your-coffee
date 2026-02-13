using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandUsePoisonDestroyConsumable : HandUseConsumable
{
    public override void Use(Hand hand)
    {
        Debug.Log("Consumables GOGOG");
        GameState.Instance.ChangeCurrentCoffeeCharacteristics();
    }
}
