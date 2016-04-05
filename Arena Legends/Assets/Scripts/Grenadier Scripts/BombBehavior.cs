using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BombBehavior : MonoBehaviour 
{

    public float finishedTime;
    public float damage;
    public bool init = false;
    bool stop = false;
    public float timer;
    public float blastRadius;
    public float distFromCenter;

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (!init) 
        {
            Init();
        }

        if (Time.time >= finishedTime && !stop) // If checks passed then the bomb has reached its timer and has not been collided with yet.
        {
            // TODO: Implement.

        }
	
	}

    void Init() 
    {
        finishedTime = Time.time + timer;
        init = true;

    }

    // This may trigger the explosion. This depends on the type of grenade it is. If this causes the explosion then the the bool stop needs to be set to true.
    void OnCollisionEnter(Collision col)
    {

    }

    public void SetDamage(float value) 
    {
        this.damage = value;
    }

    public void SetTimer(float time) 
    {
        this.timer = time;
    }

    public void SetBlastRadius(float radius) 
    {
        this.blastRadius = radius;
    }

    void Explode() 
    {
        // Get everyone within blastRadius.
        Collider[] col = Physics.OverlapSphere(this.transform.position, blastRadius); // List of all colliders within the blast range.

        // Apply damage/effect(based on whether they are friend or foe) depending on their distance from epicenter.
        foreach (Collider c in col) 
        {
            // Check if c is a player.
            int layerMask = LayerMask.NameToLayer("Player");
            if (c.gameObject.layer == layerMask) 
            {
                // Apply effect and force.
                Rigidbody rb = c.GetComponent<Rigidbody>();
                rb.AddExplosionForce(damage, this.transform.position, this.blastRadius);
            }

            // Check if c is an enemy.
            layerMask = LayerMask.NameToLayer("Enemy");
            if (c.gameObject.layer == layerMask) 
            {
                // Apply effect and force.
                Rigidbody rb = c.GetComponent<Rigidbody>();
                rb.AddExplosionForce(damage, this.transform.position, this.blastRadius);
            }

        }
        // Destroy this gameObject and delay destroy and smoke particles associated with this gameObject.
        Destroy(this.gameObject, .25f);
    }

}
