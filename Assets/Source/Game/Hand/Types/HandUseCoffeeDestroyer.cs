using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandUseCoffeeDestroyer : HandUseConsumable
{
    [SerializeField] private ParticleSystem _particle;
    [SerializeField] private bool _show = true;

    public override void Use(Hand hand)
    {
    }

    public override void UseOnAnimation()
    {
        _particle.Play();
        GameState.Instance.Coffee.DestroyCup(_show);
    }
}