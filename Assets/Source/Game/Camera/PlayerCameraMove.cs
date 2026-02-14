using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlayerCameraMove : MonoBehaviour
{
    [SerializeField] private Transform _startPoint;
    [SerializeField] private Ease _ease;
    [SerializeField] private float _speed;

    private void Start()
    {
        transform.DOMove(_startPoint.position, _speed).SetEase(_ease);
        transform.DORotate(_startPoint.eulerAngles, _speed).SetEase(_ease);
    }
}