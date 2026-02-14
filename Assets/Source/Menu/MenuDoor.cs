using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuDoor : MonoBehaviour
{
    [SerializeField] private Transform _camera;
    [SerializeField] private Transform _cameraTarget;

    private void OnMouseDown()
    {
        GetComponent<Collider>().enabled = false;
        _camera.transform.DOMove(_cameraTarget.position, 1f).OnComplete(() =>
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        });
    }
}