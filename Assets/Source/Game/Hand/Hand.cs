using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    [field: SerializeField] public List<Consumable> Consumables { get; private set; } = new List<Consumable>();

    [field: SerializeField] public Transform HideConsumablePoint { get; private set; }
}