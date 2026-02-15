using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyHealth : Health
{
    [SerializeField] private GameObject _prize;
    [SerializeField] private GameObject _prizeOnTable;

    public bool RestartPermanently;

    private void OnEnable()
    {
        if (PlayerPrefs.GetInt("Prize", 0) == 1)
        {
            _prizeOnTable.SetActive(true);
        }
    }

    public override void Death()
    {
        GetComponent<Animator>().SetTrigger("Death");
    }

    public void DeathAnimEnd()
    {
        if (RestartPermanently)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        _prize.SetActive(true);
        PlayerPrefs.SetInt("Prize", 1);
    }
}