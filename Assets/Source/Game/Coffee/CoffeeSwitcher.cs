using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeSwitcher : MonoBehaviour
{
    [SerializeField] private Coffee[] _coffies;

    public void RollCoffies()
    {
        for (int i = 0; i < _coffies.Length; i++)
        {
            _coffies[i].gameObject.SetActive(false);
            _coffies[i].transform.localPosition = new Vector3(0, 0, 0);
        }

        int count = Random.Range(1, _coffies.Length);

        for (int i = 0; i < _coffies.Length; i++)
        {
            _coffies[i].gameObject.SetActive(true);
        }
    }
}