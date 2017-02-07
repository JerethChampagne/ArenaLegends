using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBehaviour : MonoBehaviour 
{

    public List<GameObject> targets;
    public List<GameObject> activeBullets;
    public GameObject bulletPreFab;
    public GameObject bulletStartPos;
    public float TimeBetweenShots;
    public float TimeOfNextShot;
    public float bulletSpeed;

	// Use this for initialization
	void Start () 
    {
        bulletStartPos = GameObject.Find("EndBarrel");
        targets = new List<GameObject>();
        activeBullets = new List<GameObject>();
        TimeOfNextShot = Time.time + TimeBetweenShots;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (targets.Count > 0 && targets[0] == null) 
        {
            targets.RemoveAt(0);
        }
        // Face a target if there is one.
        if (targets.Count > 0 && targets[0] != null) 
        {
            // Face the target first.
            Vector3 dir = targets[0].transform.position - this.transform.position;
            this.transform.right = dir.normalized;
        }

        if (Time.time >= TimeOfNextShot && targets.Count > 0)
        {
            // Shoot a bullet.
            ShootABullet();
            // Update TimeOfNextShot.
            TimeOfNextShot = Time.time + TimeBetweenShots;
        }

	}

    void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Enemy")) 
        {
            targets.Add(other.gameObject);
        }
        

    }

    void OnTriggerExit(Collider other) 
    {
        targets.Remove(other.gameObject);
    }


    

    void ShootABullet() 
    {
        // Make sure that there are some enemies to shoot at.
        if (targets.Count > 0) 
        {
            
            // Instantiate a bullet.
            GameObject GO = MonoBehaviour.Instantiate(bulletPreFab, bulletStartPos.transform) as GameObject;
            activeBullets.Add(GO);
            
            // Give the bullet a target in its BulletBehaviour script.
            BulletBehaviour BB = GO.GetComponent<BulletBehaviour>();
            BB.target = targets[0];
            BB.speed = bulletSpeed;

            targets.Remove(targets[0]);
        }
    }
}
