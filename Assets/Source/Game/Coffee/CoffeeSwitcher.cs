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
    private bool _suppressEvents = false;

    public event Action CoffeeSwitched;

    public void RollCoffies(bool suppressEvent = false)
    {
        _suppressEvents = suppressEvent;
        _moveTween?.Kill();
        _moveTween = null;

        _currentId = 0;

        for (int i = 0; i < _coffies.Length; i++)
        {
            if (_coffies[i] != null)
            {
                _coffies[i].gameObject.SetActive(false);
                _coffies[i].transform.localPosition = Vector3.zero;
                _coffies[i].transform.localEulerAngles = Vector3.zero;
            }
        }

        _count = Random.Range(2, _coffies.Length + 1);

        for (int i = 0; i < _count; i++)
        {
            if (_coffies[i] != null)
                _coffies[i].gameObject.SetActive(true);
        }

        if (_coffies[0] != null && _activeCoffeePoint != null)
        {
            _currentCoffee = _coffies[0];
            _moveTween = _currentCoffee.transform.DOMove(_activeCoffeePoint.position, 1f);
            _currentCoffee.GetComponent<Collider>().enabled = true;

            if (GameState.Instance != null)
            {
                GameState.Instance.Coffee = _currentCoffee;
                if (!_suppressEvents)
                    CoffeeSwitched?.Invoke();
            }
        }

        _suppressEvents = false;
    }

    public bool NextCoffee() // Теперь возвращает bool!
    {
        _currentId++;
        Debug.Log(_currentId);
        Debug.Log(_count - 1);

        if (_currentId > _count - 1)
        {
            if (GameState.Instance != null)
                GameState.Instance.ReStart();
            _currentId = 0;
            return true; // Вернули TRUE - был рестарт!
        }

        if (_coffies[_currentId] != null && _activeCoffeePoint != null)
        {
            _moveTween?.Kill();
            _currentCoffee = _coffies[_currentId];
            _moveTween = _currentCoffee.transform.DOMove(_activeCoffeePoint.position, 1f);
            _currentCoffee.GetComponent<Collider>().enabled = true;

            if (GameState.Instance != null)
            {
                GameState.Instance.Coffee = _currentCoffee;
                CoffeeSwitched?.Invoke();
            }
        }

        return false; // Вернули FALSE - рестарта не было
    }

    private void OnDisable()
    {
        _moveTween?.Kill();
        _moveTween = null;
    }
}