using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameState : MonoBehaviour
{
    [SerializeField] private Hand[] _hands;
    [field: SerializeField] public CoffeeSwitcher CoffeeSwitcher { get; private set; }

    public Hand CurrentHand { get; private set; }

    [field: SerializeField] public Coffee Coffee { get; set; }

    public static GameState Instance { get; private set; }


    private int _id;

    public event Action<Hand> HandSwitched;


    private void Awake()
    {
        if (Instance == this)
            return;

        Instance = this;
    }


    private void Start()
    {
        CoffeeSwitcher.RollCoffies();
        _id = Random.Range(0, _hands.Length);
        CurrentHand = _hands[_id];
        for (int i = 0; i < _hands.Length; i++)
        {
            _hands[i].Select();
            _hands[i].HandDeselected?.Invoke();
        }

        CurrentHand.HandSelected?.Invoke();
        HandSwitched?.Invoke(CurrentHand);


        Debug.Log(_id);
    }

    public void SwitchHand(int id)
    {
        CurrentHand.HandDeselected?.Invoke();
        CurrentHand = _hands[id];
        CurrentHand.HandSelected?.Invoke();
        HandSwitched?.Invoke(CurrentHand);
    }

    public void NextHand()
    {
        _id++;
        if (_id > _hands.Length - 1)
            _id = 0;


        SwitchHand(_id);
    }
}