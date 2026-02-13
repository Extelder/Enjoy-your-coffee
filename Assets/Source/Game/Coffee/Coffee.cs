using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coffee : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [field :SerializeField] public CoffeeDamageCharacteristics DamageCharacteristics { get; private set; }

    public event Action IndicatorShow;

    public void Drink(Action DrinkEnd)
    {
        StartCoroutine(Drinking(DrinkEnd));
    }

    private IEnumerator Drinking(Action action)
    {
        _animator.SetBool("Drink", true);
        var state = _animator.GetCurrentAnimatorStateInfo(0);
        yield return new WaitUntil(() =>
        {
            var state = _animator.GetCurrentAnimatorStateInfo(0);
            return state.IsName("DrinkCoffe") && state.normalizedTime >= 0.95f;
        });
        Debug.Log("Aga");
        _animator.SetBool("Drink", false);
        action?.Invoke();
    }

    public void ShowIndicator()
    {
        IndicatorShow?.Invoke();   
    }

    public void Use(Hand hand)
    {
        hand.CoffeeDrinker.Drink(this);
    }
}