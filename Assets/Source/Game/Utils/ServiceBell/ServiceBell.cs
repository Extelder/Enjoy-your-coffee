using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

[Serializable]
public struct HandBell
{
    public Hand hand;
    public Transform bellPoint;
}

public class ServiceBell : MonoBehaviour
{
    [SerializeField] private HandBell[] _handBells;

    private void Start()
    {
        GameState.Instance.HandSwitched += OnHandSwitched;
        StartCoroutine(Wait());
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(1f);
        OnHandSwitched(GameState.Instance.CurrentHand);
    }

    private void OnHandSwitched(Hand hand)
    {
        for (int i = 0; i < _handBells.Length; i++)
        {
            if (_handBells[i].hand == hand)
            {
                transform.DOMove(_handBells[i].bellPoint.position, 1);
                return;
            }
        }
    }

    private void OnDisable()
    {
        GameState.Instance.HandSwitched -= OnHandSwitched;
    }
}