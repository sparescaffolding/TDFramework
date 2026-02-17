using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public Vector3 v;
    public float health = 2f;
    public float speed;
    public int cashToGive = 5;
    public int damage = 5;
    private Manager man;
    private bool lastEnemy = false;
    
    // Start is called before the first frame update
    void Start()
    {
        man = FindFirstObjectByType<Manager>();
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
