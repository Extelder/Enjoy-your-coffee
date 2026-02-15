using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private Consumable _checkConsumable;
    [SerializeField] private Consumable _dmgBuffConsumable;
    [SerializeField] private Consumable _skipConsumable;
    [SerializeField] private Consumable _coffeeDestroyerConsumable;  // ДОБАВИЛИ!

    [SerializeField] private Hand _hand;
    [SerializeField] private Hand _otherHand;

    private bool _isProcessing = false;
    private Coroutine _currentCoroutine = null;
    private bool _waitingForCoffeeSwitchToAct = false;

    private void OnEnable()
    {
        if (_hand != null)
        {
            _hand.HandSelected += OnHandSelected;
            _hand.HandDeselected += OnHandDeselected;
            
            if (_hand.CoffeeDrinker != null)
                _hand.CoffeeDrinker.CoffeeDrinked += OnCoffeeDrinked;
        }
    }

    private void Start()
    {
        if (GameState.Instance != null)
        {
            GameState.Instance.GameRestart += OnGameRestart;
            if (GameState.Instance.CoffeeSwitcher != null)
                GameState.Instance.CoffeeSwitcher.CoffeeSwitched += OnCoffeeSwitched;
        }
    }

    private void OnCoffeeDrinked(int damageValue)
    {
        if (!this || !GameState.Instance || GameState.Instance.IsRestarting)
            return;

        if (damageValue == 0 && IsMyTurn())
        {
            _waitingForCoffeeSwitchToAct = true;
        }
    }

    private void OnCoffeeSwitched()
    {
        if (!this || !GameState.Instance || GameState.Instance.IsRestarting)
            return;

        if (!_waitingForCoffeeSwitchToAct || !IsMyTurn() || _isProcessing)
            return;

        _waitingForCoffeeSwitchToAct = false;
        
        if (GameState.Instance.CanDrink)
            StartNewTurn();
    }

    private void OnGameRestart()
    {
        OnHandDeselected();
        _waitingForCoffeeSwitchToAct = false;
    }

    private void OnHandDeselected()
    {
        if (_currentCoroutine != null)
        {
            StopCoroutine(_currentCoroutine);
            _currentCoroutine = null;
        }
        
        _isProcessing = false;
    }

    private void OnHandSelected()
    {
        if (!GameState.Instance || GameState.Instance.IsRestarting || !GameState.Instance.CanDrink)
            return;

        if (!IsMyTurn() || _isProcessing)
            return;

        StartNewTurn();
    }

    private void StartNewTurn()
    {
        if (_isProcessing || _hand == null || _hand.Consumables == null || !GameState.Instance)
            return;

        Debug.Log("🤖 ENEMY TURN STARTED");
        
        // УМНАЯ ЛОГИКА!
        Consumable checkConsumable = FindConsumableByID(_checkConsumable);
        
        if (checkConsumable != null)
        {
            _currentCoroutine = StartCoroutine(SmartCheck(checkConsumable));
        }
        else
        {
            _currentCoroutine = StartCoroutine(SmartPlay());
        }
    }

    private bool IsMyTurn()
    {
        if (!GameState.Instance || _hand == null)
            return false;

        return GameState.Instance.CurrentHand == _hand;
    }

    // НОВАЯ УМНАЯ ЛОГИКА С ПРОВЕРКОЙ!
    private IEnumerator SmartCheck(Consumable checkConsumable)
    {
        _isProcessing = true;
        bool actionCompleted = false;

        checkConsumable.PrepareToUse(_hand, () =>
        {
            if (!this || !GameState.Instance || GameState.Instance.IsRestarting)
            {
                actionCompleted = true;
                return;
            }

            if (GameState.Instance.Coffee == null)
            {
                actionCompleted = true;
                return;
            }

            int coffeeValue = GameState.Instance.Coffee.DamageCharacteristics.Value;
            Debug.Log($"☕ Coffee damage: {coffeeValue}");

            // Если кофе смертельное (1 урон) - пытаемся защититься!
            if (coffeeValue == 1)
            {
                Consumable dmgBuff = FindConsumableByID(_dmgBuffConsumable);
                
                if (dmgBuff != null)
                {
                    Debug.Log("🛡️ Using damage buff to protect!");
                    dmgBuff.PrepareToUse(_hand, () => 
                    { 
                        if (this && GameState.Instance && !GameState.Instance.IsRestarting && GameState.Instance.CanDrink)
                            GameState.Instance.Coffee.Use(_otherHand);
                        actionCompleted = true;
                    });
                }
                else
                {
                    Debug.Log("😈 Giving deadly coffee to player!");
                    if (GameState.Instance.CanDrink)
                        GameState.Instance.Coffee.Use(_otherHand);
                    actionCompleted = true;
                }
            }
            else
            {
                Debug.Log("😊 Safe coffee - drinking it myself!");
                if (GameState.Instance.CanDrink)
                    GameState.Instance.Coffee.Use(_hand);
                actionCompleted = true;
            }
        });

        yield return new WaitUntil(() => actionCompleted || (GameState.Instance && GameState.Instance.IsRestarting));
        
        _isProcessing = false;
        _currentCoroutine = null;
    }

    // НОВАЯ УМНАЯ ИГРА БЕЗ ПРОВЕРКИ!
    private IEnumerator SmartPlay()
    {
        _isProcessing = true;

        Hand targetHand;
        if (_hand.Consumables == null || _hand.Consumables.Count == 0)
        {
            Debug.Log("🎲 No consumables - just drinking!");
            if (GameState.Instance.CanDrink && GameState.Instance.Coffee != null)
            {
                targetHand = ChooseSmartTarget();
                GameState.Instance.Coffee.Use(targetHand);
            }
            _isProcessing = false;
            _currentCoroutine = null;
            yield break;
        }

        // Проверяем, есть ли кофе-деструктор
        Consumable coffeeDestroyer = FindConsumableByID(_coffeeDestroyerConsumable);
        int coffeeValue = GameState.Instance.Coffee?.DamageCharacteristics.Value ?? 0;

        // Если кофе смертельное (1 урон) И у нас есть деструктор - используем его!
        if (coffeeValue == 1 && coffeeDestroyer != null)
        {
            Debug.Log("💥 Deadly coffee detected! Destroying it!");
            bool destroyed = false;
            coffeeDestroyer.PrepareToUse(_hand, () => { destroyed = true; });
            yield return new WaitUntil(() => destroyed || (GameState.Instance && GameState.Instance.IsRestarting));
            
            _isProcessing = false;
            _currentCoroutine = null;
            yield break;
        }

        // Используем 30-50% случайных consumable'ов (умнее чем 0-100%)
        int consumablesToUse = Random.Range(Mathf.Max(0, _hand.Consumables.Count / 3), 
                                           Mathf.Max(1, _hand.Consumables.Count / 2));
        
        Debug.Log($"🎯 Using {consumablesToUse} consumables");
        
        for (int i = 0; i < consumablesToUse && i < _hand.Consumables.Count; i++)
        {
            if (GameState.Instance && GameState.Instance.IsRestarting)
                break;

            if (_hand.Consumables[i] == null)
                continue;

            bool wait = false;
            Consumable currentConsumable = _hand.Consumables[i];

            currentConsumable.PrepareToUse(_hand, () => 
            { 
                if (this && (!GameState.Instance || !GameState.Instance.IsRestarting))
                    wait = true; 
            });
            
            yield return new WaitUntil(() => wait || (GameState.Instance && GameState.Instance.IsRestarting));
            
            if (GameState.Instance && GameState.Instance.IsRestarting)
                break;

            yield return new WaitForSeconds(0.5f);
        }

        if (!this || !GameState.Instance || GameState.Instance.IsRestarting)
        {
            _isProcessing = false;
            _currentCoroutine = null;
            yield break;
        }

        // Умный выбор цели
        targetHand = ChooseSmartTarget();
        
        if (GameState.Instance.CanDrink && GameState.Instance.Coffee != null)
            GameState.Instance.Coffee.Use(targetHand);

        _isProcessing = false;
        _currentCoroutine = null;
    }

    // Умный выбор цели
    private Hand ChooseSmartTarget()
    {
        if (GameState.Instance.Coffee == null)
            return _hand;

        int coffeeValue = GameState.Instance.Coffee.DamageCharacteristics.Value;
        
        // Если кофе смертельное - даем противнику, иначе себе
        if (coffeeValue == 1)
        {
            Debug.Log("☠️ Giving deadly coffee to opponent!");
            return _otherHand;
        }
        else
        {
            Debug.Log("✅ Taking safe coffee!");
            return _hand;
        }
    }

    // Вспомогательный метод поиска
    private Consumable FindConsumableByID(Consumable template)
    {
        if (template == null || _hand.Consumables == null)
            return null;

        return _hand.Consumables.Find(c => c != null && c.ID == template.ID);
    }

    private void OnDisable()
    {
        if (_hand != null)
        {
            _hand.HandSelected -= OnHandSelected;
            _hand.HandDeselected -= OnHandDeselected;
            
            if (_hand.CoffeeDrinker != null)
                _hand.CoffeeDrinker.CoffeeDrinked -= OnCoffeeDrinked;
        }
        
        if (GameState.Instance != null)
        {
            GameState.Instance.GameRestart -= OnGameRestart;
            if (GameState.Instance.CoffeeSwitcher != null)
                GameState.Instance.CoffeeSwitcher.CoffeeSwitched -= OnCoffeeSwitched;
        }
    }
}
