using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameState : MonoBehaviour
{
    [SerializeField] private Hand[] _hands;
    [field: SerializeField] public CoffeeSwitcher CoffeeSwitcher { get; private set; }
    [SerializeField] private bool _playAlone;

    public Hand CurrentHand { get; private set; }

    [field: SerializeField] public Coffee Coffee { get; set; }

    public static GameState Instance { get; private set; }


    public bool SkipHand { get; set; }
    public int Id { get; private set; }

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
        if (_playAlone)
            Id = 0;
        else
            Id = Random.Range(0, _hands.Length);
        CurrentHand = _hands[Id];
        for (int i = 0; i < _hands.Length; i++)
        {
            _hands[i].Select();
            _hands[i].HandDeselected?.Invoke();
        }

        CurrentHand.HandSelected?.Invoke();
        HandSwitched?.Invoke(CurrentHand);


        Debug.Log(Id);
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
        if (SkipHand)
        {
            Debug.Log("SKIP");
            SkipHand = false;
            return;
        }
        Id++;
        if (Id > _hands.Length - 1)
            Id = 0;


        SwitchHand(Id);
    }
}