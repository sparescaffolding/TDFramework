using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class TowerBrain : MonoBehaviour
{
    public string name = "Tower";
    public Texture portrait;
    public Transform target;
    public float detectRange = 15f;
    public float fireRate = 1f;
    public GameObject projectilePrefab;
    public Transform firePoint;
    [Range(0, 3)] public int TopPathTier;
    public List<TowerUpgradeData> TopPathData = new List<TowerUpgradeData>();
    [Range(0, 3)] public int MiddlePathTier;
    public List<TowerUpgradeData> MiddlePathData = new List<TowerUpgradeData>();
    [Range(0, 3)] public int BottomPathTier;
    public List<TowerUpgradeData> BottomPathData = new List<TowerUpgradeData>();
    public int CrossPathLimit = 1;
    private float cooldown = 0f;
    public int cost = 20;
    public Manager man;
    public int value;
    public bool isPlaced = false;
    
    private void Start()
    {
        man = FindFirstObjectByType<Manager>();

        value = (cost * 70) / 100;
        
        isPlaced = true;
        
        if (man.cash >= cost)
        {
            //deduct cash using manager on placement
            man.Deduct(cost);
            man.StopPlacing();
            man.towerList.SetActive(true);
        }
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
        }
        else
        {
            target = null;
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
    private void ApplyUpgrade(TowerUpgradeData data)
    {
        if (man.cash >= data.cost)
        {
            man.Deduct(data.cost);
            detectRange += data.newDetectRange;
            fireRate += data.newFireRate;
            if (data.newProjectilePrefab != null)
            {
                projectilePrefab = data.newProjectilePrefab;
            }
        }
        else
        {
            Debug.Log("Poor");
            return;
        }
    }
    
    public void UpgradeTopPath()
    {
        if (TopPathTier < 3)
        {
            ApplyUpgrade(TopPathData[TopPathTier]);
            value += (TopPathData[TopPathTier].cost * 70) / 100;
            man.RefreshTowerValue();
            TopPathTier++;
        }
    }
    public void UpgradeMiddlePath()
    {
        if (MiddlePathTier < 3)
        {
            ApplyUpgrade(MiddlePathData[MiddlePathTier]);
            value += (MiddlePathData[MiddlePathTier].cost * 70) / 100;
            man.RefreshTowerValue();
            MiddlePathTier++;
        }
    }
    public void UpgradeBottomPath()
    {
        if (BottomPathTier < 3)
        {
            ApplyUpgrade(BottomPathData[BottomPathTier]);
            value += (BottomPathData[BottomPathTier].cost * 70) / 100;
            man.RefreshTowerValue();
            BottomPathTier++;
        }
    }
}
