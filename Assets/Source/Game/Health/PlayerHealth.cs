using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : Health
{
    [SerializeField] private Image _deathScreen;

    public static PlayerHealth Instance;

    private void Awake()
    {
        if (Instance == this)
            return;

        Instance = this;
    }

    public override void Death()
    {
        Debug.Log("Death");
        _deathScreen.DOFade(1, 5)
            .OnComplete(() => { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); });
    }
}