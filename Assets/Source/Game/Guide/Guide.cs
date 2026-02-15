using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guide : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private GameObject[] _guideImages;
    [SerializeField] private PlayerConsumableSelector _playerConsumableSelector;
    private int _index;
    
    public void StartGuide()
    {
        _playerConsumableSelector.enabled = false;
        _index = 0;
        _canvas.enabled = true;
        _guideImages[_index].SetActive(true);
    }

    public void NextGuide()
    {
        _guideImages[_index].SetActive(false);
        _index++;
        if (_index >= _guideImages.Length)
        {
            StopGuide();
        }
        _guideImages[_index].SetActive(true);
    }

    private void StopGuide()
    {
        _playerConsumableSelector.enabled = true;
        _index = 0;
        _canvas.enabled = false;
    }
}
