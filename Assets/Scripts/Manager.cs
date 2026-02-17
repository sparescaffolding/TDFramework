using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Manager : MonoBehaviour
{
    public int cash = 100;
    public TextMeshProUGUI cashText;
    public int health = 100;
    public TextMeshProUGUI healthText;

    private void Update()
    {
        // constantly update UI text to show current cash and health
        cashText.text = "Cash: " + cash;
        healthText.text = "Health: " + health;
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
}
