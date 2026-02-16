using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseWorldPos : MonoBehaviour
{
    public Camera camera;
    public LayerMask layer;
    public RaycastHit hit;

    private void Update()
    {
        Vector3 mouseScreenPosition = Input.mousePosition;
        Ray ray = camera.ScreenPointToRay(mouseScreenPosition);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layer))
        {
            transform.position = hit.point;
        }
    }
}
