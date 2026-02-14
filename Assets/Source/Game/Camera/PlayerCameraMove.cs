using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UniRx;

public class PlayerCameraMove : MonoBehaviour
{
    [SerializeField] private Camera _camera;

    private CompositeDisposable _disposable = new CompositeDisposable();

    private RaycastHit _hit;

    private void OnEnable()
    {
        StealConsumableHandUse.CanSteel += OnCanSteel;
        PlayerConsumableSelector.CantSteel += OnCantSteel;
    }

    private void OnCantSteel()
    {
        GetComponent<Animator>().SetBool("CameraTop", false);
    }

    private void OnCanSteel()
    {
        GetComponent<Animator>().SetBool("CameraTop", true);
    }

    public void AnimReadyToCheck()
    {
        Observable.EveryUpdate().Subscribe(_ =>
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out _hit, 100))
            {
                if (_hit.collider.TryGetComponent<Door>(out Door Door))
                {
                    Door.Open();
                    GetComponent<Animator>().SetBool("LastAnim", true);
                    _disposable?.Clear();
                }
            }
        }).AddTo(_disposable);
    }

    private void OnDisable()
    {
        _disposable?.Clear();
        StealConsumableHandUse.CanSteel -= OnCanSteel;
        PlayerConsumableSelector.CantSteel -= OnCantSteel;
    }
}