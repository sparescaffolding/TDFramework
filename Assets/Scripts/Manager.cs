using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Manager : MonoBehaviour
{
    public int cash = 100;
    public TextMeshProUGUI cashText;

    private void Update()
    {
        cashText.text = "Cash: " + cash;
    }

    public void Deduct(int amount)
    {
        cash -= amount;
    }

    public void AddCash(int amount)
    {
        cash += amount;
    }
}
