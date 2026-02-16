using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public Vector3 v;
    public float speed;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //move forward at a constant speed
        transform.Translate(v.normalized * Time.deltaTime * speed, Space.World);
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
}
