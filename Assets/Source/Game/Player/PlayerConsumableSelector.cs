using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerConsumableSelector : MonoBehaviour
{
    [SerializeField] private Hand _hand;
    [SerializeField] private Hand _otherHand;
    [SerializeField] private Camera _camera;

    private RaycastHit _hit;

    public bool CanIntereact = true;

    public event Action ConsumableUsed;
    public static event Action CantSteel;

    private void OnEnable()
    {
        _hand.HandSelected += OnHandSelected;
        _hand.HandDeselected += OnHandDeselected;
    }

    private void OnHandSelected()
    {
        CanIntereact = true;
    }

    private void OnHandDeselected()
    {
        CanIntereact = false;
    }

    private void OnDisable()
    {
        _hand.HandSelected -= OnHandSelected;
        _hand.HandDeselected -= OnHandDeselected;
    }

    private void Update()
    {
        if (!CanIntereact)
            return;
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out _hit, 100))
        {
            if (_hit.collider.TryGetComponent<Consumable>(out Consumable Consumable))
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    if (_hand.CanSteal)
                    {
                        Consumable.PrepareToUse(_hand);
                        CantSteel?.Invoke();
                        _hand.CanSteal = false;
                    }
                    else if (Consumable.Hand == _hand)
                        Consumable.PrepareToUse(_hand);

                    ConsumableUsed?.Invoke();
                }
            }

            if (_hit.collider.TryGetComponent<Coffee>(out Coffee Coffee))
            {
                if (_hand.CanSteal)
                {
                    return;
                }

                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    Coffee.Use(_hand);
                }

                if (Input.GetKeyDown(KeyCode.Mouse1))
                {
                    Coffee.Use(_otherHand);
                }
            }
        }
    }
}