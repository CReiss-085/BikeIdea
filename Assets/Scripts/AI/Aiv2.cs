using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Aiv2 : SelfDespawn
{


    //Variables that can be set when the new abstract Enemy is contructed 
    public int startingHP;
    public int speed;
    public int attackRange;
    public int score;
    public bool alive;

    //complex 
    public Rigidbody rb;
    public Gun myGun;
    public CyborgAnimationStateController animationStateController;
    public GameObject target;
    public Health hp;


    public override void Init()
    {
        
        #region Error Checking
        if (animationStateController == null)
        {
            Debug.LogError("This object needs a CyborgAnimationStateController component");
        }
        if (rb == null)
        {
            Debug.LogError("This object needs a RigidBody component");
        }
        if (myGun == null)
        {
            Debug.LogError("This object needs a GUN component");
        }
        if (hp == null)
        {
            Debug.LogError("This object needs a Health component");
        }
        if (target == null)
        {
            Debug.LogError("This object needs a Target to Pursue");
        }
        #endregion
        throw new System.NotImplementedException();
    }

    public override void Update()
    {
        base.Update();//Checks if AI is out of bounds 
        

        animationStateController.SetSpeed(rb.velocity.magnitude);
        if (hp.HitPoints <= 0) //this signifies that the enemy Died and wasn't merely Despawned 
        {
            myGun.StopAllCoroutines();
            animationStateController.StopAllCoroutines();
            alive = false;
            this.gameObject.SetActive(false);
        }
        else
        {
            Move(target.transform.position);
        }
    }

    public abstract void Move(Vector3 t);

    public void applyForce(Vector3 force)
    {
        rb.AddForce(force);
    }

    public abstract void Attack();
    public abstract void SetStats();

    #region Getters&Setters  
    public bool isAlive()
    {
        return alive;
    }
    public float getScore()
    {
        return score;
    }
    #endregion
}

public class gruntAi : Aiv2
{
    public void Awake()
    {
        alive = true;
        hp = GetComponentInChildren<Health>();
        hp.Init(startingHP);
    }
    public override void Attack()
    {
        if (myGun.CanShootAgain())
        {
            this.myGun.Shoot(target.transform.position);
            //TODO: the AI currently doesn't stand still when firing and I'm not sure if they should?
            animationStateController.Shoot();
        }
        throw new System.NotImplementedException();
    }

    public override void Move(Vector3 target)
    {
        Vector3 desiredVec = target - transform.position; //this logic creates the vector between where the entity is and where it wants to be 
        float dMag = desiredVec.magnitude; //this creates a magnitude of the desired vector. This is the distance between the points 
        dMag -= attackRange; // dmag is the distance between the two objects, by subtracking this, I make it so the object doesn't desire to move as far.  
        desiredVec.Normalize(); // one the distance is measured this vector can now be used to actually generate movement,
                                // but that movement has to be constant or at least adaptable, which is what the next part does  
        transform.LookAt(target);

        //Currently Walking twoards the target 

        if (dMag < speed)
        {
            desiredVec *= dMag; //Slowing down walking speed as AI approaches Target 
            if (dMag < attackRange)
            {
                Attack();
            }
        }
        else
        {
            desiredVec *= speed;
        }

        Vector3 steer = desiredVec - rb.velocity;
        applyForce(steer);


        throw new System.NotImplementedException();
    }

    public override void SetStats()
    {
        startingHP = 60;
        speed = 10;
        attackRange = 10;
        score = 69;
    }

    public void loadout(GameObject player, Gun gun)
    {
        target = player;
        myGun = gun;
    }


}



public class bikerAi : Aiv2
{

    public void Awake()
    {
        alive = true; 
        hp = GetComponentInChildren<Health>();
        hp.Init(startingHP);
    }

    public override void Attack()
    {
        throw new System.NotImplementedException();
    }//TODO

    public override void Move(Vector3 target)
    {
        //Vroom Vroom Drive me Bike 

        throw new System.NotImplementedException();


    }//TODO

    public override void SetStats()
    {
        startingHP = 20;
        speed = 10;
        attackRange = 10;
        score = 69;


        throw new System.NotImplementedException();
    }//TODO

}
