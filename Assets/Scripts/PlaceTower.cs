using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlaceTower : MonoBehaviour
{
    private MouseWorldPos ms;
    public List<GameObject> towerList = new List<GameObject>();
    private GameObject towerSelected;
    public bool place = false;
    private Manager man;
    private bool canafford;
    private TowerBrain b;

    private void Start()
    {
        ms = GetComponent<MouseWorldPos>();
        man = FindFirstObjectByType<Manager>();
    }
    public void Update()
    {
        b = towerSelected.GetComponent<TowerBrain>();
        //if left click is pressed, can place, can afford and mouse is not over UI
        if (Input.GetMouseButtonDown(0) && place && !EventSystem.current.IsPointerOverGameObject() && towerSelected != null && b != null)
        {
            if (b.cost <= man.cash)
            {
                Instantiate(towerSelected, ms.hit.point, Quaternion.identity);

                man.Deduct(b.cost);
                man.StopPlacing();

                place = false;
            }
            else
            {
                Debug.Log("poor....");
            }
        }
    }
    public void PlaceTrue()
    {
        place = true;
    }

    public void SelectTower(int index)
    {
        towerSelected = towerList[index];
        if (b.cost <= man.cash)
        {
            canafford = true;
            place = true;
        }
        else
        {
            canafford = false;
            place = false;
        }
    }
}
