using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandUseHandSkipper : HandUseConsumable
{
    public override void Use(Hand hand)
    {
    }

    public override void UseOnAnimation()
    {
        GameState.Instance.SkipHand = true;
    }
}
