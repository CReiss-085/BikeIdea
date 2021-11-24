﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager : MonoBehaviour
{
    public GameObject ground;
    public BikeScript bike;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ApplyForces();
        UpdateLocations();
    }

    private void UpdateFloorLocation() 
    {
        Material groundMat = ground.GetComponent<Renderer>().material;
        groundMat.SetFloat("_XPos", -bike.GetPosition().x);
        groundMat.SetFloat("_YPos", bike.GetPosition().y);
        Debug.Log(bike.GetPosition());
    }

    private void ApplyForces() 
    {
        bike.ApplyForces();
    }

    private void UpdateLocations() 
    {
        UpdateFloorLocation();
        bike.UpdateLocations();
    }
}
