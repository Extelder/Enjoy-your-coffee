using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    [field: SerializeField] public Coffee Coffee { get; private set; }

    public static GameState Instance { get; private set; }

    private void OnEnable()
    {
        if (Instance == this)
            return;

        Instance = this;
    }

    public void ChangeCurrentCoffeeCharacteristics()
    {
        Debug.Log("COFFEE NOT POISONOUS");
        Coffee.CoffeePoisonous = false;
    }
}