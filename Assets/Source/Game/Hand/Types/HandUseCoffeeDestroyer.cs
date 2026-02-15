using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandUseCoffeeDestroyer : HandUseConsumable
{
    [SerializeField] private ParticleSystem _particle;
    
    public override void Use(Hand hand)
    {
    }

    public override void UseOnAnimation()
    {
        _particle.Play();
        if (GameState.Instance.Coffee.DamageCharacteristics.Value == 0)
        {
            return;
        }
        GameState.Instance.Coffee.DestroyCup();
    }
}
