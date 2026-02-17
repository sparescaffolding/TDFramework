using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlaceTower : MonoBehaviour
{
    private MouseWorldPos ms;
    public GameObject tower;
    public bool place = false;
    private Manager man;
    private bool canafford;

    private void Start()
    {
        ms = GetComponent<MouseWorldPos>();
        man = FindFirstObjectByType<Manager>();
    }

    public void Update()
    {
        TowerBrain b = tower.GetComponent<TowerBrain>();
        
        // money checks
        if (b.cost <= man.cash)
        {
            canafford = true;
        }
        else
        {
            canafford = false;
        }
        
        if (canafford)
        {
            place = true;
        }
        else
        {
            place = false;
        }
        
        //if left click is pressed, can place, can afford and mouse is not over UI
        if (Input.GetMouseButtonDown(0) && ms.CanPlace && place && canafford && !EventSystem.current.IsPointerOverGameObject())
        {
            //if can afford
            if (b.cost <= man.cash)
            {
                //buy and place tower
                Debug.Log("Tower 1");
                Instantiate(tower, ms.hit.point, Quaternion.identity);
                place = false;
            }
            else
            {
                Debug.Log("poor....");
            }
        }
    }
}
