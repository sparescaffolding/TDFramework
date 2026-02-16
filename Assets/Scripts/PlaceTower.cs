using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlaceTower : MonoBehaviour
{
    private MouseWorldPos ms;
    public GameObject tower;

    private void Start()
    {
        ms = GetComponent<MouseWorldPos>();
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Tower 1");
            Instantiate(tower, ms.hit.point, Quaternion.identity);
        }
    }
}
