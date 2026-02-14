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
    private bool _canMove = true;

    public Hand Hand;

    public abstract void Use(Hand hand);

    public void Init(Hand hand)
    {
        Hand = hand;
    }

    public virtual void PrepareToUse(Hand hand)
    {
        if (!_canMove)
            return;
        _canMove = false;
        _tween = transform.DOMove(hand.HideConsumablePoint.position, _moveTime).SetEase(_ease).OnComplete(() =>
        {
            _canMove = true;
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