using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullScreenSettings : MonoBehaviour
{
    private PlayerConfig _config;

    private void Start()
    {
        _config = PlayerConfig.Instance;
        Screen.fullScreen = _config.ConfigData.fullScreen;
    }

    public void SwitchFullScreenSettings()
    {
        Screen.fullScreen = !Screen.fullScreen;
        _config.ConfigData.fullScreen = Screen.fullScreen;
        _config.Save();
    }
}
