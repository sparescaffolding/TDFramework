using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RoundData", menuName = "Data/TowerUpgradeData")]
public class TowerUpgradeData : ScriptableObject
{
    public string name;
    public int cost;
    public float newDetectRange;
    public float newFireRate;
    public GameObject newProjectilePrefab;
}