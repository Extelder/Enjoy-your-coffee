using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

[Serializable]
public struct PlayableConsumable
{
    public Consumable Consumable;
    public HandUseConsumable ConsumableObject;
}

public class Hand : MonoBehaviour
{
    [field: SerializeField] public CoffeeDrinker CoffeeDrinker { get; private set; }
    
    [field: SerializeField] public List<Consumable> Consumables { get; private set; } = new List<Consumable>();

    [field: SerializeField] public Transform HideConsumablePoint { get; private set; }
    [field: SerializeField] public PlayableConsumable[] PlayableConsumables { get; private set; }

    public void PlayConsumable(Consumable consumable)
    {
        for (int i = 0; i < PlayableConsumables.Length; i++)
        {
            int cA = PlayableConsumables[i].Consumable.ID;
            int cB = consumable.ID;
            Debug.Log(cA == cB);

            if (cA == cB)
            {
                Debug.Log("Consumables Play");
                PlayableConsumables[i].ConsumableObject.gameObject.SetActive(true);
                PlayableConsumables[i].ConsumableObject.Use(this);
            }
        }
    }
}