using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPool : SelfDespawn
{
    // Visuals
    private GameObject player;
    public int Spawndistance = 800;
    public int SpawnAngleRandomNess = 60;
    public float SizeofCylinder = 400;
    public float RateOfDecay = 20f;
    private const float DEFAULT_MIN_SCALE = 50.0f;
    private const float DEFAULT_MAX_SCALE = 400.0f;
    private const float DEFULAT_SCALE_SHRINK_PER_SECOND = 20f;
    private const float PLAYER_HEAL_AMNT = 100f;

    private float minScale;
    private float maxScale;
    private float shrinkPerSecond;
    private float curScale;


    
    

    /// <summary>The percentage of how "complete" this pool is.</summary>
    public float PercentFull 
    {
        get 
        {
            float scaleFromBottom = curScale - minScale;
            float maxDiff = maxScale - minScale;
            return scaleFromBottom / maxDiff;
        }
    }

    private void Start()
    {

        player = GameObject.Find("Player Bike");
        Init();


    }


    private void Update()
    {
        if (curScale <= minScale)
        {
            OnDespawn();
        }
        else
        {
            // Shrink if player is in the pool
            Shrink(shrinkPerSecond * Time.deltaTime);
        }
    }

    /// <summary>Reinitializes and turns on this HealthPool gameObject.</summary>
    /// <param name="shrinkPerSecond">The amount at which the scale is reduced per second.</param>
    /// <param name="startScale">The scale at which this healthpool starts at.</param>
    /// <param name="minScale">The minimum scale which the healthpool can shrink to before it is despawned.</param>
    public void Init(float shrinkPerSecond = DEFULAT_SCALE_SHRINK_PER_SECOND, float startScale = DEFAULT_MAX_SCALE, float minScale = DEFAULT_MIN_SCALE) 
    {
        this.maxScale = startScale;
        this.minScale = minScale;
        this.shrinkPerSecond = shrinkPerSecond;

        SetScale(startScale);

        this.transform.position = SpawnVector(player.transform.right,SpawnAngleRandomNess,Spawndistance);

        this.gameObject.SetActive(true);
    }

    protected override void OnDespawn() 
    {
        this.gameObject.SetActive(false);
        base.OnDespawn();
        Init(RateOfDecay,SizeofCylinder);
    }

    /// <summary>
    /// Should call the default Init method.
    /// </summary>
    public override void Init()
    {
        this.Init();
    }


    /// <summary>Updates the current scale.</summary>
    /// <param name="scale">The scale at which to set this object to.</param>
    private void SetScale(float scale)
    {
        curScale = ClampScale(scale);
        Vector3 objScale = new Vector3(curScale, 1, curScale);

        transform.localScale = objScale;
    }

    /// <summary>Shrinks the regen scale by amnt.</summary>
    /// <param name="amnt">The amount of points to reduce curScale by.</param>
    private void Shrink(float amnt)
    {
        SetScale(curScale - amnt);
    }

    /// <summary>Clamps amnt between MIN_SCALE and MAX_SCALE.</summary>
    /// <param name="amnt">The unit to be clamped.</param>
    /// <returns>The clamped amnt.</returns>
    private float ClampScale(float amnt)
    {
        return Mathf.Clamp(amnt, minScale, maxScale);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Health playerHealthRef = other.GetComponentInChildren<Health>();
            playerHealthRef.Heal(PLAYER_HEAL_AMNT);
            OnDespawn();
        }
    }

    /// <summary> //TODO Make this part of static utill class
    /// This method returns a vector 
    /// </summary>
    /// <param name="bias"> this is the direction that the bike is already moving </param>
    /// <param name="angle"> the range of degrees that the vector can be rotated to ( 0 to 180 ) </param>
    /// <param name="distance"> the desired lenght of the spawn vector </param>
    /// <returns></returns>
    public Vector3 SpawnVector(Vector3 bias, int angle, int distance)
    {
        if (bias == new Vector3(0, 0, 0))// defaut case if bike isn't moving 
        {
            bias = new Vector3(0, 0, 1);
        }

        Vector3 spawnVector = bias;
        Quaternion q = Quaternion.Euler(0, Random.Range(-angle, angle), 0);

        spawnVector = q * spawnVector;

        spawnVector.Normalize();
        spawnVector *= distance;
        spawnVector += player.transform.position;
        return spawnVector;
    }

}
