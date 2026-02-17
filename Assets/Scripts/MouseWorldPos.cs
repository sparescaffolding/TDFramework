using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseWorldPos : MonoBehaviour
{
    public Camera camera;
    public LayerMask placeLayer;
    public RaycastHit hit;
    public bool CanPlace;

    private void Update()
    {
        // point ray at where mouse is on screen
        Vector3 mouseScreenPosition = Input.mousePosition;
        Ray ray = camera.ScreenPointToRay(mouseScreenPosition);

        //if mouse is over 
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, placeLayer))
        {
            CanPlace = true;
            transform.position = hit.point;
        }
        else
        {
            CanPlace = false;
        }
    }
}
