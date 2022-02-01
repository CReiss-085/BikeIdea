using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GruntAi : Aiv2
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
