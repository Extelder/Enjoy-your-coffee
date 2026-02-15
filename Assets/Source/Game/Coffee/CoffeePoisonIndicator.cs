using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeePoisonIndicator : MonoBehaviour
{
    [SerializeField] private Coffee _coffee;
    [SerializeField] private GameObject _image;
    [SerializeField] private float _cooldownToRecover;

    private void OnEnable()
    {
        _coffee.IndicatorShow += OnIndicatorShow;
    }

    private void OnIndicatorShow()
    {
        _image.SetActive(true);
        StartCoroutine(IndicatorShowing());
    }

    private IEnumerator IndicatorShowing()
    {
        yield return new WaitForSeconds(_cooldownToRecover);
        _image.SetActive(false);
    }

    private void OnDisable()
    {
        _image.SetActive(false);
        _coffee.IndicatorShow -= OnIndicatorShow;
        StopAllCoroutines();
    }
}