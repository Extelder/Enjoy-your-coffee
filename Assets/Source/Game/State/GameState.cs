using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameState : MonoBehaviour
{
    [SerializeField] private Hand[] _hands;
    [SerializeField] private CoffeeSwitcher _coffeeSwitcher;

    public Hand CurrentHand { get; private set; }

    [field: SerializeField] public Coffee Coffee { get; private set; }

    public static GameState Instance { get; private set; }


    private int _id;


    private void Awake()
    {
        if (Instance == this)
            return;

        Instance = this;
    }


    private void Start()
    {
        _coffeeSwitcher.RollCoffies();
        _id = Random.Range(0, _hands.Length);
        CurrentHand = _hands[_id];
        CurrentHand.Select();
    }

    public void SwitchHand(int id)
    {
        CurrentHand = _hands[id];
        CurrentHand.Select();
    }

    public void NextHand()
    {
        _id++;
        if (_id > _hands.Length - 1)
            _id = 0;

        SwitchHand(_id);
    }

    public void ChangeCurrentCoffeeCharacteristics()
    {
        Debug.Log("COFFEE NOT POISONOUS");
        Coffee.CoffeePoisonous = false;
    }
}