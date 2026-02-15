using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HandUseConsumable : MonoBehaviour
{
    public abstract void Use(Hand hand);
    public abstract void UseOnAnimation();

    public void Disable()
    {
        gameObject.SetActive(false);
    }
}
