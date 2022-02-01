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

