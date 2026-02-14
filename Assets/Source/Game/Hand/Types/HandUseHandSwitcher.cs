using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandUseHandSwitcher : HandUseConsumable
{
    public override void Use(Hand hand)
    {
        GameState.Instance.SwitchHand(GameState.Instance.Id + 1);
    }
}
