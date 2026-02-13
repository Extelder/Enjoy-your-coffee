using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public abstract class Consumable : MonoBehaviour
{
    [SerializeField] private float _moveTime = 1;
    [SerializeField] private Ease _ease = Ease.InOutQuint;
    [field: SerializeField] public int ID { get; private set; }

    private Tween _tween;

    public abstract void Use(Hand hand);

    public virtual void PrepareToUse(Hand hand)
    {
        _tween = transform.DOMove(hand.HideConsumablePoint.position, _moveTime).SetEase(_ease).OnComplete(() =>
        {
            Use(hand);
            hand.Consumables.Remove(this);
            Destroy(gameObject);
        });
    }

    private void OnDisable()
    {
        _tween.Kill();
    }
}