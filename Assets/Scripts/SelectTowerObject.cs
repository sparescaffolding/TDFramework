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
            man.UpgradeTopInfo();
            man.UpdatePortrait();
            man.CrossPathManager();
        }
    }
    public void UpgradeMiddlePath()
    {
        if (selectedTower != null)
        {
            selectedTower.UpgradeMiddlePath();
            man.UpgradeMiddleInfo();
            man.UpdatePortrait();
            man.CrossPathManager();
        }
    }
    public void UpgradeBottomPath()
    {
        if (selectedTower != null)
        {
            selectedTower.UpgradeBottomPath();
            man.UpgradeBottomInfo();
            man.UpdatePortrait();
            man.CrossPathManager();
        }
    }
    public void SellTower()
    {
        man.AddCash(selectedTower.value);
        man.ExitUpgradePanel();
        Destroy(selectedTower.gameObject);
    }
}