using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    // fields
    public List<GameObject> enemies = new List<GameObject>();
    public float cooldown = 0.5f;
    public GameObject startButton;
    
    public void StartRound()
    {
        //start round immediately
        //to implement: dedicated round start button
        InvokeRepeating(nameof(SpawnEnemies), 0f, cooldown);
        startButton.SetActive(false);
    }

    // Update is called once per frame
    void SpawnEnemies()
    {
        //for every enemy in enemies list
        foreach (GameObject v in enemies)
        {
            //if count is LARGER than 0
            if (enemies.Count > 0)
            {
                //spawn enemy prefab
                Instantiate(enemies[0], gameObject.transform.position, gameObject.transform.rotation);
                //remove enemy from list that was just spawned
                enemies.RemoveAt(0);
            }
            else
            {
                // stop spawning when list is empty
                CancelInvoke(nameof(SpawnEnemies));
            }
        }
    }

    private void Update()
    {
        if (enemies.Count <= 0)
        {
            startButton.SetActive(true);
        }
    }
}
