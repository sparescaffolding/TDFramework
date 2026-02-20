using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    public int cash = 100;
    public TextMeshProUGUI cashText;
    public int health = 100;
    public TextMeshProUGUI healthText;
    public GameObject startButton;
    public GameObject speedButton;
    public RoundManager roundManager;
    private PlaceTower pctower;
    private MouseWorldPos player;
    public GameObject towerList;
    public Color colorTwiceSpeed;
    private Color speedButtonColor;
    public bool TwoTimesSpeed = false;
    private Image speedButtonImage;
    public GameObject upgradePanel;
    public GameObject exitUpgradeButton;
    public TextMeshProUGUI towerSellValueText;
    public TextMeshProUGUI towerName;
    public SelectTowerObject selectedTowerObject;

    private void Start()
    {
        roundManager = FindFirstObjectByType<RoundManager>();
        pctower = FindFirstObjectByType<PlaceTower>();
        player = FindFirstObjectByType<MouseWorldPos>();
        selectedTowerObject = FindFirstObjectByType<SelectTowerObject>();
        speedButtonImage = speedButton.GetComponent<Image>();
        speedButtonColor = speedButtonImage.color;
        Time.timeScale = 1;
    }
    private void Update()
    {
        // constantly update UI text to show current cash and health
        cashText.text = "Cash: " + cash;
        healthText.text = "Health: " + health;

        if (!TwoTimesSpeed)
        {
            Time.timeScale = 1;
            speedButtonImage.color = speedButtonColor;
        }
        else
        {
            Time.timeScale = 2;
            speedButtonImage.color = colorTwiceSpeed;
        }
    }
    public void Deduct(int amount)
    {
        //deduct cash by amount
        cash -= amount;
        //make sure cash never goes below 0
        if (cash <= 0)
        {
            cash = 0;
        }
    }
    public void AddCash(int amount)
    {
        //add cash by amount specified
        cash += amount;
    }
    public void RemoveHP(int amount)
    {
        //remove hp by amount specified
        health -= amount;
        //make sure health never goes below 0
        if (health <= 0)
        {
            health = 0;
        }
    }
    //may never use, but still useful to have just in case needed
    public void AddHP(int amount)
    {
        //add cash by amount specified
        health += amount;
    }
    public void RoundEnded()
    {
        startButton.SetActive(true);
        speedButton.SetActive(false);
        AddCash(roundManager.rounds[0].moneyToGive);
        roundManager.rounds.RemoveAt(0);
    }
    public void ToggleSpeed()
    {
        TwoTimesSpeed = !TwoTimesSpeed;
    }
    public void StopPlacing()
    {
        towerList.SetActive(true);
        player.CanPlace = false;
        pctower.place = false;
    }
    public void UpgradePanel()
    {
        towerSellValueText.text = "$" + selectedTowerObject.selectedTower.value;
        towerName.text = selectedTowerObject.selectedTower.name;
        upgradePanel.SetActive(true);
        towerList.SetActive(false);
        exitUpgradeButton.SetActive(true);
    }
    public void ExitUpgradePanel()
    {
        upgradePanel.SetActive(false);
        towerList.SetActive(true);
        exitUpgradeButton.SetActive(false);
    }
    public void CrossPathManager()
    {
        if (selectedTowerObject.selectedTower.TopPathTier >= 2 && selectedTowerObject.selectedTower.MiddlePathTier != 0 || selectedTowerObject.selectedTower.BottomPathTier != 0)
        {
            Debug.Log("T2 M0 B0");
        }
    }
}
