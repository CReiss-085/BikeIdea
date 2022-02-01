using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : MonoBehaviour
{
    #region EnemyAI lists

    public List<GruntAi> Riflemen;
    public GruntAi grunt;
    public float gruntWaveSize;


    public List<Gun> guns;


    #endregion

    public GameObject player;
    public int spawnDistance = 30;

    #region Getters&Setters
    public Vector3 generateSpawnVector()
    {
        Vector3 spawnVector = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z + spawnDistance);
        Quaternion ranRot = Quaternion.Euler(0, Random.Range(0, 359), 0);

        spawnVector = ranRot * spawnVector;

        return spawnVector;
    }
    public Gun getGun()
    {
        return guns[0];
    }//TODO Hard code in list of all guns 
    #endregion

    
    public void op_KILLME(SelfDespawn entity) // THis is the method that sets the entity to Deactive and bascially is uesd to kill the entitiy 
    {
        entity.gameObject.SetActive(false);
        //TODO: Add Logic here to make sure Entity either remains in the pool or becomes a new entity
    }

    void Start()
    {

        
        for(int i=0; i < Riflemen.Count; i++)
        {
            GruntAi newEnemy = Instantiate(grunt, generateSpawnVector(), Quaternion.identity);
            newEnemy.loadout(player, getGun());
            newEnemy.Init();
            
            newEnemy.Despawn += op_KILLME; //this line adds the despawn event to this entity 
            newEnemy.gameObject.SetActive(true);

            Riflemen.Add(newEnemy);
        }

        //TODO add Stuff here for BIKER 
    }

    public void Respawn(Aiv2 dedDude)
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
