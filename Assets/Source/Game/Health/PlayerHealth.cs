using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health
{
    public static PlayerHealth Instance;
    
    private void Awake()
    {
        if (Instance == this)
            return;
        
        Instance = this;
    }

    public override void Death()
    {
    }
}
