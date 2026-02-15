using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{
    [SerializeField] private GameObject _prize;

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