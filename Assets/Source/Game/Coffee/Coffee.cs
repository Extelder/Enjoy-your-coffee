using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coffee : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [field: SerializeField] public CoffeeDamageCharacteristics DamageCharacteristics { get; private set; }
    private bool _canUse = true;

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
        if (DamageCharacteristics.Value > 0)
            IndicatorShow?.Invoke();
    }

    private void OnDisable()
    {
        StopAllCoroutines();
        _canUse = true;
        GetComponent<Collider>().enabled = false;
    }

    public void DestroyCup()
    {
        Debug.Log("SHOW INDICATOR");
        ShowIndicator();
        StartCoroutine(Destroying());
    }

    private IEnumerator Destroying()
    {
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
        GameState.Instance.CoffeeSwitcher.NextCoffee();
    }

    public void Use(Hand hand)
    {
        if (!_canUse)
            return;
        _canUse = false;
        hand.CoffeeDrinker.Drink(this);
    }
}