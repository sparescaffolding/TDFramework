using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, placeLayer, !EventSystem.IsPointerOverGameObject()))
        {
            //set position to where mouse is
            CanPlace = true;
            transform.position = hit.point;
        }
        else
        {
            CanPlace = false;
        }
    }
}
