using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class AnimationConsumable : Consumable
{
    public override void Use(Hand hand)
    {
        hand.PlayConsumable(this);
    }
}