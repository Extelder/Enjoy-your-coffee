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

    public event Action CoffiesEnd;
    public event Action CoffieSwitched;

    public void RollCoffies()
    {
        for (int i = 0; i < _coffies.Length; i++)
        {
            _coffies[i].gameObject.SetActive(false);
            _coffies[i].transform.localPosition = new Vector3(0, 0, 0);
        }

        int count = Random.Range(1, _coffies.Length);

        for (int i = 0; i < _coffies.Length; i++)
        {
            _coffies[i].gameObject.SetActive(true);
        }

        _currentCoffee = _coffies[0];
        _currentCoffee.transform.DOMove(_activeCoffeePoint.position, 1);
        GameState.Instance.Coffee = _currentCoffee;
    }

    public void NextCoffee()
    {
        _currentId++;
        if (_currentId > _coffies.Length - 1)
        {
            CoffiesEnd?.Invoke();
            return;
        }

        _currentCoffee = _coffies[_currentId];
        _currentCoffee.transform.DOMove(_activeCoffeePoint.position, 1);
        GameState.Instance.Coffee = _currentCoffee;
        CoffieSwitched?.Invoke();
    }
}