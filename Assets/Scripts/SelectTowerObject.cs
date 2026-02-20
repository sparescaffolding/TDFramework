using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectTowerObject : MonoBehaviour
{
    public Camera camera;
    public LayerMask placeLayer;
    public RaycastHit hit;
    public TowerBrain selectedTower;
    public Manager man;

    private void Start()
    {
        man = FindFirstObjectByType<Manager>();
    }

    private void Update()
    {
        // point ray at where mouse is on screen
        Vector3 mouseScreenPosition = Input.mousePosition;
        Ray ray = camera.ScreenPointToRay(mouseScreenPosition);

        //if mouse is over 
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, placeLayer))
        {
            if (Input.GetMouseButtonDown(0))
            {
                TowerBrain tower = hit.transform.GetComponent<TowerBrain>();
                selectedTower = tower;
                if (selectedTower.isPlaced)
                {
                    man.UpgradePanel();
                }
                Debug.Log("click " + tower.name);
            }
        }
    }

    public void UpgradeTopPath()
    {
        if (selectedTower != null)
        {
            selectedTower.UpgradeTopPath();
        }
    }
    public void UpgradeMiddlePath()
    {
        if (selectedTower != null)
        {
            selectedTower.UpgradeMiddlePath();
        }
    }
    public void UpgradeBottomPath()
    {
        if (selectedTower != null)
        {
            selectedTower.UpgradeBottomPath();
        }
    }
}