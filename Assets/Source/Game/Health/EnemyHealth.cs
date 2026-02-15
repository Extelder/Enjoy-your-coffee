using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{
    [SerializeField] private GameObject _prize;
    [SerializeField] private GameObject _prizeOnTable;

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
        _prize.SetActive(true);
        PlayerPrefs.SetInt("Prize", 1);
    }
}