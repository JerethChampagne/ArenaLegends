using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour 
{
    public GameObject player;

    private Vector3 offset;
    

	// Use this for initialization
	void Start () 
    {
        offset = transform.position - player.transform.position;
		
	}

    void Update() 
    {

    }
	
	// LateUpdate is called once per frame
	void LateUpdate () 
    {
        // Give the camera a new position first.
        this.transform.position = player.transform.position + offset;

	}
}
