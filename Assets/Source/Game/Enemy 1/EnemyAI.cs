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

    [SerializeField] private Hand _hand;
    [SerializeField] private Hand _otherHand;

    private bool _handSelected;

    private void OnEnable()
    {
        _hand.HandSelected += OnHandSelected;
        _hand.HandDeselected += OnHandDeselected;
    }

    private void OnHandDeselected()
    {
        _handSelected = false;
    }

    private void OnHandSelected()
    {
        _handSelected = true;
        if (_hand.Consumables.Find(consumable => consumable.ID == _checkConsumable.ID))
        {
            _hand.Consumables.Find(consumable => consumable.ID == _checkConsumable.ID).PrepareToUse(_hand, () =>
            {
                if (GameState.Instance.Coffee.DamageCharacteristics.Value == 1)
                {
                    if (_hand.Consumables.Find(consumable => consumable.ID == _dmgBuffConsumable.ID))
                    {
                        _hand.Consumables.Find(consumable => consumable.ID == _dmgBuffConsumable.ID).PrepareToUse(_hand,
                            () => { GameState.Instance.Coffee.Use(_otherHand); });
                    }
                    else
                    {
                        GameState.Instance.Coffee.Use(_otherHand);
                    }
                }
                else
                {
                    GameState.Instance.Coffee.Use((_hand));
                    OnHandSelected();
                }
            });
        }
        else
        {
            Debug.Log("Rnadom Shit");
            StartCoroutine(RandomShit());
        }
    }

    private IEnumerator RandomShit()
    {
        bool skiphand = false;
        int randomConsumablesTOUse = Random.Range(0, _hand.Consumables.Count - 1);
        for (int i = 0; i < randomConsumablesTOUse; i++)
        {
            bool wait = false;
            if (_hand.Consumables[i].ID == _skipConsumable.ID)
            {
                skiphand = true;
            }

            _hand.Consumables[i].PrepareToUse(_hand, () => { wait = true; });
            yield return new WaitUntil(() => wait);
        }

        if (Random.Range(0, 10) > 5)
        {
            GameState.Instance.Coffee.Use(_hand);
        }
        else
        {
            GameState.Instance.Coffee.Use(_otherHand);
        }

        if (GameState.Instance.Coffee.DamageCharacteristics.Value == 0 || skiphand)
            OnHandSelected();
    }

    private void OnDisable()
    {
        _hand.HandSelected -= OnHandSelected;
        _hand.HandDeselected -= OnHandDeselected;
    }
}