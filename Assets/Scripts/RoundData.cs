using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RoundData", menuName = "Data/RoundData")]
public class RoundData : ScriptableObject
{
    public List<GameObject> enemies = new List<GameObject>();
    public int moneyToGive;
}
