using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reflector : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        //draw in inspector the reflection direction of the reflector object
        Gizmos.color = Color.blue; 
        Vector3 start = transform.position;                  
        Vector3 end = transform.position + transform.forward * 2f; 
        Gizmos.DrawLine(start, end);
    }

}
