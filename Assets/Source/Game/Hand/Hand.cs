using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

[Serializable]
public struct PlayableConsumable
{
    public Consumable Consumable;
    public GameObject ConsumableObject;
}

public class Hand : MonoBehaviour
{
    [field: SerializeField] public List<Consumable> Consumables { get; private set; } = new List<Consumable>();

    [field: SerializeField] public Transform HideConsumablePoint { get; private set; }
    [field: SerializeField] public PlayableConsumable[] PlayableConsumables { get; private set; }

    public void PlayConsumable(Consumable consumable)
    {
        for (int i = 0; i < PlayableConsumables.Length; i++)
        {
            Consumable cA = PlayableConsumables[i].Consumable as Consumable;
            Consumable cB = consumable.ConsumablePrefab as Consumable;
            Debug.Log(cA == cB);

            if (cA == cB)
            {
                PlayableConsumables[i].ConsumableObject.SetActive(true);
            }
        }
    }
}