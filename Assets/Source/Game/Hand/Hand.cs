using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

[Serializable]
public struct PlayableConsumable
{
    public Consumable Consumable;
    public HandUseConsumable ConsumableObject;
}

public class Hand : MonoBehaviour
{
    [SerializeField] private Transform _discardPoint;
    [field: SerializeField] public CoffeeDrinker CoffeeDrinker { get; private set; }

    [field: SerializeField] public List<Consumable> Consumables { get; private set; } = new List<Consumable>();
    [field: SerializeField] public Transform[] ConsumableSlots { get; private set; }

    [field: SerializeField] public Transform HideConsumablePoint { get; private set; }
    [field: SerializeField] public PlayableConsumable[] PlayableConsumables { get; private set; }

    [SerializeField] private Consumable[] _consumablePrefabs;

    public bool CanSteal = false;
    
    public Action HandSelected;
    public Action HandDeselected;

    public void Select()
    {
        if (Consumables.Count > 0)
        {
            for (int i = 0; i < Consumables.Count; i++)
            {
                Consumable consumable = Consumables[i];

                Consumables[i].transform.DOMove(_discardPoint.position, 1)
                    .OnComplete(() => { Destroy(consumable.gameObject); });
                Consumables.Remove(Consumables[i]);
            }
        }

        Invoke(nameof(Roll), 1);
    }

    private void Roll()
    {
        for (int i = 0; i < ConsumableSlots.Length; i++)
        {
            Consumables.Add(Instantiate(_consumablePrefabs[Random.Range(0, _consumablePrefabs.Length)],
                _discardPoint.position,
                Quaternion.identity));
            Consumables[i].transform.DOMove(ConsumableSlots[i].position, 1);
            Consumables[i].Init(this);
        }
    }

    public void PlayConsumable(Consumable consumable)
    {
        for (int i = 0; i < PlayableConsumables.Length; i++)
        {
            int cA = PlayableConsumables[i].Consumable.ID;
            int cB = consumable.ID;

            if (cA == cB)
            {
                PlayableConsumables[i].ConsumableObject.gameObject.SetActive(true);
                PlayableConsumables[i].ConsumableObject.Use(this);
            }
        }
    }
}