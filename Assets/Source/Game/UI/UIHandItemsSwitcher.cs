using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHandItemsSwitcher : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;
    
    private void OnMouseEnter()
    {
        _canvas.enabled = true;
    }

    private void OnMouseExit()
    {
        _canvas.enabled = false;
    }
}
