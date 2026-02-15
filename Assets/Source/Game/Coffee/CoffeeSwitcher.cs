using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class CoffeeSwitcher : MonoBehaviour
{
    [SerializeField] private Coffee[] _coffies;

    [SerializeField] private Transform _activeCoffeePoint;

    private Coffee _currentCoffee;

    private int _currentId;


    private int _count;

    private Tween _moveTween;

    public void RollCoffies()
    {
        _currentId = 0;
        for (int i = 0; i < _coffies.Length; i++)
        {
            _coffies[i].gameObject.SetActive(false);
            _coffies[i].transform.localPosition = new Vector3(0, 0, 0);
            _coffies[i].transform.localEulerAngles = new Vector3(0, 0, 0);
        }

        _count = Random.Range(1, _coffies.Length + 1);

        for (int i = 0; i < _count; i++)
        {
            _coffies[i].gameObject.SetActive(true);
            _moveTween?.Kill();
        }

        _currentCoffee = _coffies[0];
        _moveTween = _currentCoffee.transform.DOMove(_activeCoffeePoint.position, 1);
        GameState.Instance.Coffee = _currentCoffee;
    }

    public void NextCoffee()
    {
        _currentId++;
        Debug.Log(_currentId);
        Debug.Log((_count - 1));
        if (_currentId > _count - 1)
        {
            GameState.Instance.ReStart();
            _currentId = 0;
            return;
        }

        _currentCoffee = _coffies[_currentId];
        _moveTween = _currentCoffee.transform.DOMove(_activeCoffeePoint.position, 1);
        GameState.Instance.Coffee = _currentCoffee;
    }

    private void OnDisable()
    {
        _moveTween?.Kill();
    }
}