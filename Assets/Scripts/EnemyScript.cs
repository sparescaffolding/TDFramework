using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public Vector3 v;
    public float health = 2f;
    public float speed;
    public int cashToGive = 5;
    public int damage = 5;
    public List<GameObject> spawnOnDeath =  new List<GameObject>();
    private Manager man;
    private RoundManager roundManager;
    private bool lastEnemy = false;
    public bool isChild;
    
    // Start is called before the first frame update
    void Start()
    {
        man = FindFirstObjectByType<Manager>();
        roundManager = FindObjectOfType<RoundManager>();
        
        if(isChild)
        {
            roundManager.spawnedEnemies.Add(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //move forward at a constant speed
        transform.Translate(v.normalized * Time.deltaTime * speed, Space.World);
        //if health 0 then die
        if (health <= 0f)
        {
            man.AddCash(cashToGive);
            if (lastEnemy)
            {
                man.RoundEnded();
                man.startButton.SetActive(true);
            }

            float spacing = 1.05f;
            int index = 0;

            if (spawnOnDeath.Count > 0)
            {
                foreach (GameObject e in spawnOnDeath)
                {
                    Vector3 offset = -v.normalized * index * spacing;
                    //new enemy to spawn
                    GameObject n = Instantiate(e, transform.position + offset, transform.rotation);

                    EnemyScript script = n.GetComponent<EnemyScript>();
                    if (script != null)
                    {
                        //have same vector3 values as me
                        script.v = this.v;
                    }

                    index++;
                }
            }

            spawnOnDeath.Clear();
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Reflector")
        {
            Debug.Log("reflected");
            //reflect and continue moving with the direction in accordance of the forward direction of the reflector
            v = other.transform.forward;
        }
    }

    public void TakeDamage(float damage)
    {
        //deduct health
        health -= damage;
    }

    public void EnableStartButton()
    {
        lastEnemy = true;
    }
}
