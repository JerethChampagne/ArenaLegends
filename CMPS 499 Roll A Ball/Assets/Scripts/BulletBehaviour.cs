using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour 
{

    public float speed;

    public GameObject target;


	// Use this for initialization
	void Start () 
    {
		
	}
	
	// Update is called once per frame
	void Update () 
    {
		// Get direction vector.
        Vector3 dir = target.transform.position - this.transform.position;

        // Move in that direction.
        this.transform.position += dir.normalized * speed * Time.deltaTime;
	}

    void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag("Enemy")) 
        {
            other.gameObject.BroadcastMessage("TakeDamage", 1000f);
        }

        MonoBehaviour.Destroy(this.gameObject);
    }
}
