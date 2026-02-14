using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStart : MonoBehaviour
{
    [SerializeField] private Animator _animatorEnemy;
    private void EnemyAnimStart()
    {
        _animatorEnemy.SetTrigger("Start?");
    }
}
