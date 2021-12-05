﻿using UnityEngine;

public class MovementManager : MonoBehaviour
{
    public GameObject ground;
    public BikeScript bike;
    public GameObject Cactus;
    public CactusScript cacscrip;
    public GameObject[] cacti;
    public Vector3 preVec;
    public Vector3 curVec;
    private float uvScrollSpeed = .013f;

    // Start is called before the first frame update
    void Start()
    {

        

        //Spawn Cactai 
        cacti = new GameObject[10];

        for (int i = 0; i < 10; i++)
        {
            Vector3 spawnP = new Vector3(Random.Range(-80, 80), -2, Random.Range(-80, 80));
            cacti[i] = Instantiate(Cactus, spawnP, Quaternion.identity);
            cacti[i].GetComponent<CactusScript>().grow(spawnP);
            
            //print(cacti[i].transform.position);

        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       
        ApplyForces();
        UpdateLocations();
    }

    private void UpdateFloorLocation() //Update the floor mesh to reflect movement
    {
        Material groundMat = ground.GetComponent<Renderer>().material;
        groundMat.SetFloat("_XPos", -bike.GetPosition().x * uvScrollSpeed);
        groundMat.SetFloat("_YPos", bike.GetPosition().y * uvScrollSpeed);
        //Debug.Log(bike.GetPosition());
    }


    private void UpdateCactiLocation()
    {
 
        //Vector3 offset = curVec - preVec;
        
        for(int i = 0; i<10; i++)
        {
            cacti[i].GetComponent<CactusScript>().move(curVec);
            
        }

    }

    private void BikePositionLastFrame()
    {
        
    }

    private void ApplyForces()
    {      
        bike.ApplyForces();       
    }

    private void UpdateLocations()
    {
        
        
        preVec = new Vector3(bike.position.x, 0, bike.position.y);
        bike.UpdateLocations();
        curVec = new Vector3(bike.position.x, 0, bike.position.y);

        UpdateCactiLocation();
        UpdateFloorLocation();
    }
}
