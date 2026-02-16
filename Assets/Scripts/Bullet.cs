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
        target = target2;
        moveDirection = (target.position - transform.position).normalized;
    }
    
    void Update()
    {
        float distanceNow = speed * Time.deltaTime;
        transform.Translate(moveDirection.normalized * distanceNow, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            EnemyScript b = other.GetComponent<EnemyScript>();
            b.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
