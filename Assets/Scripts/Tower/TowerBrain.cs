using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class TowerBrain : MonoBehaviour
{
    public Transform target;
    public float detectRange = 15f;
    public float fireRate = 1f;
    public bool shooting;
    public GameObject projectilePrefab;
    public Transform firePoint;
    private float cooldown = 0f;
    public int cost = 20;
    public Manager man;

    private void Start()
    {
        man = FindFirstObjectByType<Manager>();
        if (man.cash >= cost)
        {
            //deduct cash using manager on placement
            man.Deduct(cost);
            man.StopPlacing();
            man.towerList.SetActive(true);
        }
        
        man.StopPlacing();
    }

    private void FixedUpdate()
    {
        //update info on targets every frame, probably the most unoptimized way
        UpdateTarget();
    }

    void UpdateTarget()
    {
        //get list of all enemies that exist
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float shortestDistance = Mathf.Infinity;
        GameObject nearestenemy = null;
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestenemy = enemy;
            }
        }

        if (nearestenemy != null && shortestDistance <= detectRange)
        {
            target = nearestenemy.transform;
            shooting = true;
        }
        else
        {
            target = null;
            shooting = false;
        }
    }
    
    private void Update()
    {
        //if there is no target
        if (target == null)
        {
            //do nothing
            return;
        }
        
        Vector3 direction = target.position - transform.position;
        direction.y = 0f;

        //look at target in the y axis.
        transform.rotation = Quaternion.LookRotation(direction);
        
        //shoot logic
        //if cooldown is over
        if (cooldown <= 0f)
        {
            //call shoot function
            Shoot();
            //reset cooldown
            cooldown = 1f / fireRate;
        }
        
        cooldown -= Time.deltaTime;
    }

    private void Shoot()
    {
        Debug.Log("tower " + gameObject.name + " is shooting...");
        //create projectile prefab
        GameObject projectile = (GameObject)Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        //get bullet component from spawned prefab
        Bullet b = projectile.GetComponent<Bullet>();
        //if projectile spawned, call seek function in projectile bullet script
        b.Seek(target);
    }

    private void OnDrawGizmosSelected()
    {
        //render range sphere in scene editor
        Gizmos.DrawWireSphere(transform.position, detectRange);
        Gizmos.color = Color.blue;
    }
}
