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

    private bool _canDrink = true;
    public bool CanDrink
    {
        get => _canDrink;
        set => _canDrink = value;
    }

    private bool _isRestarting = false;
    public bool IsRestarting => _isRestarting;

    public event Action<Hand> HandSwitched;
    public event Action GameRestart;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        if (CoffeeSwitcher == null)
        {
            Debug.LogError("CoffeeSwitcher is null!");
            return;
        }

        CoffeeSwitcher.RollCoffies(false);
        
        if (_playAlone)
            Id = 0;
        else
            Id = Random.Range(0, _hands.Length);
        
        CurrentHand = _hands[Id];
        
        for (int i = 0; i < _hands.Length; i++)
        {
            if (_hands[i] != null)
            {
                _hands[i].Select();
                if (i != Id)
                    _hands[i].HandDeselected?.Invoke();
            }
        }

        CurrentHand?.HandSelected?.Invoke();
        HandSwitched?.Invoke(CurrentHand);

        Debug.Log(Id);
    }

    public void ReStart()
    {
        _isRestarting = true;
        _canDrink = false;
    
        GameRestart?.Invoke();
    
        // Деактивируем все руки
        for (int i = 0; i < _hands.Length; i++)
        {
            if (_hands[i] != null)
                _hands[i].HandDeselected?.Invoke();
        }
    
        // Перезапускаем кофе с подавлением события
        if (CoffeeSwitcher != null)
            CoffeeSwitcher.RollCoffies(true);
    
        // Обновляем карты для всех рук
        for (int i = 0; i < _hands.Length; i++)
        {
            if (_hands[i] != null)
                _hands[i].Select();
        }
    
        // Устанавливаем руку игрока
        Id = 0;
        CurrentHand = _hands[0];
    
        // ВАЖНО: Сначала вызываем события, ПОТОМ разрешаем пить!
        CurrentHand?.HandSelected?.Invoke();
        HandSwitched?.Invoke(CurrentHand);
    
        // Только ПОСЛЕ всех событий разрешаем игру
        _isRestarting = false;
        _canDrink = true;
    
        Debug.Log("RESTART - Player turn started");
    }

    public void SwitchHand(int id)
    {
        if (id < 0 || id >= _hands.Length || _hands[id] == null)
            return;

        if (CurrentHand != null && CurrentHand != _hands[id])
            CurrentHand.HandDeselected?.Invoke();
        
        CurrentHand = _hands[id];
        CurrentHand.HandSelected?.Invoke();
        HandSwitched?.Invoke(CurrentHand);
    }

    public void NextHand()
    {
        if (SkipHand)
        {
            CurrentHand?.HandSelected?.Invoke();
            Debug.Log("SKIP");
            SkipHand = false;
            return;
        }

        Id++;
        if (Id > _hands.Length - 1)
            Id = 0;

        SwitchHand(Id);
    }

    private void OnDestroy()
    {
        if (Instance == this)
            Instance = null;
    }
}
