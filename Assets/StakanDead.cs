using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StakanDead : MonoBehaviour
{
    private void OnMouseDown()
    {
        GameState.Instance.Undless();
    }
}