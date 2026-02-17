using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;

    public float damage = 2f;
    
    public float speed = 10f;
    
    private Vector3 moveDirection;

    
    public void Seek(Transform target2)
    {
        //find target specified from function call
        target = target2;
        //and start going towards it
        moveDirection = (target.position - transform.position).normalized;
    }
    
    void Update()
    {
        float distanceNow = speed * Time.deltaTime;
        //move forward at speed
        transform.Translate(moveDirection.normalized * distanceNow, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        //if collides into an enemy
        if (other.tag == "Enemy")
        {
            //get enemy script from enemy
            EnemyScript b = other.GetComponent<EnemyScript>();
            //deduct health from enemy by damage specified in the inspector
            b.TakeDamage(damage);
            //destroy bullet (or projectile)
            Destroy(gameObject);
        }
    }
}
