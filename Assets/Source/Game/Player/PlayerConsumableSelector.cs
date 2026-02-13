using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerConsumableSelector : MonoBehaviour
{
    [SerializeField] private Hand _hand;
    [SerializeField] private Camera _camera;

    private RaycastHit _hit;

    private void Update()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out _hit, 100))
        {
            if (_hit.collider.TryGetComponent<Consumable>(out Consumable Consumable))
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    Consumable.PrepareToUse(_hand);
                }
            }

            if (_hit.collider.TryGetComponent<Coffee>(out Coffee Coffee))
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    Coffee.Use(_hand);
                }
            }
        }
    }
}