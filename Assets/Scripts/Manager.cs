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
    public RawImage towerPortrait;
    public SelectTowerObject selectedTowerObject;
    [Header("UpgradeButtonTop")]
    public TextMeshProUGUI upgradeButtonNameTop;
    public TextMeshProUGUI upgradeButtonCostTop;
    public RawImage upgradeButtonIconTop;
    public Button upgradeButtonTop;
    [Header("UpgradeButtonMiddle")]
    public TextMeshProUGUI upgradeButtonNameMiddle;
    public TextMeshProUGUI upgradeButtonCostMiddle;
    public RawImage upgradeButtonIconMiddle;
    public Button upgradeButtonMiddle;
    [Header("UpgradeButtonBottom")]
    public TextMeshProUGUI upgradeButtonNameBottom;
    public TextMeshProUGUI upgradeButtonCostBottom;
    public RawImage upgradeButtonIconBottom;
    public Button upgradeButtonBottom;
    

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
        RefreshTowerValue();
        towerName.text = selectedTowerObject.selectedTower.name;
        upgradePanel.SetActive(true);
        UpdatePortrait();
        UpgradeTopInfo();
        UpgradeMiddleInfo();
        UpgradeBottomInfo();
        CrossPathManager();
        towerList.SetActive(false);
        exitUpgradeButton.SetActive(true);
    }
    public void ExitUpgradePanel()
    {
        upgradePanel.SetActive(false);
        towerList.SetActive(true);
        exitUpgradeButton.SetActive(false);
    }

    public void RefreshTowerValue()
    {
        towerSellValueText.text = "$" + selectedTowerObject.selectedTower.value;
    }
    public void CrossPathManager()
    {
        //top cross 2
        if (selectedTowerObject.selectedTower.TopPathTier == 0 && selectedTowerObject.selectedTower.MiddlePathTier == 0 && selectedTowerObject.selectedTower.BottomPathTier == 0)
        {
            upgradeButtonTop.interactable = true;
            upgradeButtonMiddle.interactable = true;
            upgradeButtonBottom.interactable = true;
        }
        if (selectedTowerObject.selectedTower.TopPathTier >= 2 && selectedTowerObject.selectedTower.BottomPathTier == 1)
        {
            upgradeButtonMiddle.interactable = false;
        }
        if (selectedTowerObject.selectedTower.TopPathTier >= 2 && selectedTowerObject.selectedTower.MiddlePathTier == 1)
        {
            upgradeButtonBottom.interactable = false;
        }
        //middle cross 2
        if (selectedTowerObject.selectedTower.MiddlePathTier >= 2 && selectedTowerObject.selectedTower.BottomPathTier == 1)
        {
            upgradeButtonTop.interactable = false;
        }
        if (selectedTowerObject.selectedTower.MiddlePathTier >= 2 && selectedTowerObject.selectedTower.TopPathTier == 1)
        {
            upgradeButtonBottom.interactable = false;
        }
        //bottom cross 2
        if (selectedTowerObject.selectedTower.BottomPathTier >= 2 && selectedTowerObject.selectedTower.TopPathTier == 1)
        {
            upgradeButtonMiddle.interactable = false;
        }
        if (selectedTowerObject.selectedTower.BottomPathTier >= 2 && selectedTowerObject.selectedTower.MiddlePathTier == 1)
        {
            upgradeButtonTop.interactable = false;
        }
    }
    public void UpdatePortrait()
    {
        if (selectedTowerObject.selectedTower.TopPathTier > selectedTowerObject.selectedTower.BottomPathTier && selectedTowerObject.selectedTower.TopPathTier > selectedTowerObject.selectedTower.MiddlePathTier)
        {
            towerPortrait.texture = selectedTowerObject.selectedTower.TopPathData[selectedTowerObject.selectedTower.TopPathTier].newPortrait;
        }
        if (selectedTowerObject.selectedTower.MiddlePathTier > selectedTowerObject.selectedTower.BottomPathTier && selectedTowerObject.selectedTower.MiddlePathTier > selectedTowerObject.selectedTower.TopPathTier)
        {
            towerPortrait.texture = selectedTowerObject.selectedTower.MiddlePathData[selectedTowerObject.selectedTower.MiddlePathTier].newPortrait;
        }
        if (selectedTowerObject.selectedTower.BottomPathTier > selectedTowerObject.selectedTower.TopPathTier && selectedTowerObject.selectedTower.BottomPathTier > selectedTowerObject.selectedTower.MiddlePathTier)
        {
            towerPortrait.texture = selectedTowerObject.selectedTower.BottomPathData[selectedTowerObject.selectedTower.BottomPathTier].newPortrait;
        }
    }
    //UPGRADE BUTTON UI MANAGEMENT
    public void UpgradeTopInfo()
    {
        //name
        upgradeButtonNameTop.text = selectedTowerObject.selectedTower
            .TopPathData[selectedTowerObject.selectedTower.TopPathTier].name;
        //cost
        upgradeButtonCostTop.text = "$" + selectedTowerObject.selectedTower
            .TopPathData[selectedTowerObject.selectedTower.TopPathTier].cost;
        //icon
        upgradeButtonIconTop.texture = selectedTowerObject.selectedTower
            .TopPathData[selectedTowerObject.selectedTower.TopPathTier].icon;
    }
    public void UpgradeMiddleInfo()
    {
        //name
        upgradeButtonNameMiddle.text = selectedTowerObject.selectedTower
            .MiddlePathData[selectedTowerObject.selectedTower.MiddlePathTier].name;
        //cost
        upgradeButtonCostMiddle.text = "$" + selectedTowerObject.selectedTower
            .MiddlePathData[selectedTowerObject.selectedTower.MiddlePathTier].cost;
        //icon
        upgradeButtonIconMiddle.texture = selectedTowerObject.selectedTower
            .MiddlePathData[selectedTowerObject.selectedTower.MiddlePathTier].icon;
    }
    public void UpgradeBottomInfo()
    {
        //name
        upgradeButtonNameBottom.text = selectedTowerObject.selectedTower
            .BottomPathData[selectedTowerObject.selectedTower.BottomPathTier].name;
        //cost
        upgradeButtonCostBottom.text = "$" + selectedTowerObject.selectedTower
            .BottomPathData[selectedTowerObject.selectedTower.BottomPathTier].cost;
        //icon
        upgradeButtonIconBottom.texture = selectedTowerObject.selectedTower
            .BottomPathData[selectedTowerObject.selectedTower.BottomPathTier].icon;
    }
}
