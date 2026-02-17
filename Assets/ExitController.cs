using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitController : MonoBehaviour
{
    public Manager man;
    
    private void OnTriggerEnter(Collider other)
    {
        //if enemy goes through
        if (other.tag == "Enemy")
        {
            //get enemy script from enemy
            EnemyScript e = other.GetComponent<EnemyScript>();
            //remove HP
            man.RemoveHP(e.damage);
            //destroy enemy
            Destroy(other.gameObject);
        }
    }
}
