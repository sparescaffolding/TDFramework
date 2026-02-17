using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    // fields
    public List<RoundData> rounds = new List<RoundData>();
    public List<GameObject> enemies = new List<GameObject>();
    public float cooldown = 0.5f;
    public Manager man;
    public GameObject startButton;
    private bool roundActive = false;

    private List<GameObject> spawnedEnemies = new List<GameObject>();

    private void Start()
    {
        man = FindFirstObjectByType<Manager>();
    }

    public void StartRound()
    {
        enemies = new List<GameObject>(rounds[0].enemies);
        
        // start round immediately
        InvokeRepeating(nameof(SpawnEnemies), 0f, cooldown);
        startButton.SetActive(false);
        roundActive = true;
    }

    // Update is called once per frame
    void SpawnEnemies()
    {
        // if no enemies left → stop round
        if (enemies.Count == 0)
        {
            // stop spawning when list is empty
            CancelInvoke(nameof(SpawnEnemies));
            return;
        }

        // spawn enemy prefab
        GameObject enemy = Instantiate(enemies[0], gameObject.transform.position, gameObject.transform.rotation);
        spawnedEnemies.Add(enemy);

        // remove enemy from list that was just spawned
        enemies.RemoveAt(0);

        if (enemies.Count == 1)
        {
            EnemyScript b = enemies[0].gameObject.GetComponent<EnemyScript>();
            b.EnableStartButton();
        }
    }

    private void Update()
    {
        if (!roundActive)
            return;

        spawnedEnemies.RemoveAll(e => e == null);

        if (enemies.Count == 0 && spawnedEnemies.Count == 0)
        {
            startButton.SetActive(true);
            man.RoundEnded();
            roundActive = false;
        }
    }
}