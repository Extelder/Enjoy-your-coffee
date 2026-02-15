using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField] private Canvas _settingsCanvas;

    private void CanvasEnable()
    {
        _settingsCanvas.enabled = !_settingsCanvas.enabled;
        if (_settingsCanvas.enabled)
        {
            Time.timeScale = 0;
            StopAllCoroutines();
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CanvasEnable();
        }
    }
}