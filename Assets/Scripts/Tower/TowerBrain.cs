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
            man.Deduct(cost);
        }
    }

    private void FixedUpdate()
    {
        UpdateTarget();
    }

    void UpdateTarget()
    {
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
        if (target == null)
        {
            return;
        }
        
        Vector3 direction = target.position - transform.position;
        direction.y = 0f;

        transform.rotation = Quaternion.LookRotation(direction);
        //shoot logic
        if (cooldown <= 0f)
        {
            Shoot();
            cooldown = 1f / fireRate;
        }
        
        cooldown -= Time.deltaTime;
    }

    private void Shoot()
    {
        Debug.Log("shooting...");
        GameObject projectile = (GameObject)Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Bullet b = projectile.GetComponent<Bullet>();
        
        if(projectile != null)
            b.Seek(target);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, detectRange);
        Gizmos.color = Color.blue;
    }
}
