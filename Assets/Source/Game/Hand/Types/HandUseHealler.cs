using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandUseHealler : HandUseConsumable
{
    [SerializeField] private int _amountToHeal;
    
    public override void Use(Hand hand)
    {
    }

    public override void UseOnAnimation()
    {
        PlayerHealth.Instance.Heal(_amountToHeal);
    }
}
