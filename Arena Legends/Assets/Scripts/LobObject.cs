using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LobObject : MonoBehaviour 
{
    public GameObject obj;
    public Vector3 endPoint;
    public bool init = false;
    public Vector3 startPoint;
    public float speed = 10.0f;
    public Vector3 direction;
    public Rigidbody rb;
    public float timeToTarget;

	// Use this for initialization
	void Start () 
    {
	    
	}
	
	// Update is called once per frame
	void Update () 
    {
        //GetDirection();
        if (Input.GetKeyDown(KeyCode.Alpha0)) 
        {
            ThrowObject();
        }

	}

    void GetDirection() 
    {
        // Get the direction the object needs to go.
        direction = endPoint - startPoint;
        direction.Normalize(); // Normalize the vector so the magnitude is 1.
    }

    void Move() 
    {
        transform.position += speed * Time.deltaTime * direction;
    }

    public void Init(Vector3 end, float endTime) 
    {
        if (init)
            return;

        this.timeToTarget = endTime;
        //this.obj = obj;
        this.endPoint = end;
        this.startPoint = transform.position;
        return;

    }

    public void ThrowObject() 
    {
        rb.AddForce(CalculateBestThrowSpeed(startPoint, endPoint, timeToTarget), ForceMode.VelocityChange);
    }


    Vector3 CalculateBestThrowSpeed(Vector3 origin, Vector3 target, float timeToTarget) 
    {
     
         // calculate vectors
         Vector3 toTarget = target - origin;
         Vector3 toTargetXZ = toTarget;
         toTargetXZ.y = 0;
     
         // calculate xz and y
         float y = toTarget.y;
         float xz = toTargetXZ.magnitude;
     
         // calculate starting speeds for xz and y. Physics forumulase deltaX = v0 * t + 1/2 * a * t * t
         // where a is "-gravity" but only on the y plane, and a is 0 in xz plane.
         // so xz = v0xz * t => v0xz = xz / t
         // and y = v0y * t - 1/2 * gravity * t * t => v0y * t = y + 1/2 * gravity * t * t => v0y = y / t + 1/2 * gravity * t
         float t = timeToTarget;
         float v0y = y / t + 0.5f * Physics.gravity.magnitude * t;
         float v0xz = xz / t;
     
         // create result vector for calculated starting speeds
         Vector3 result = toTargetXZ.normalized;        // get direction of xz but with magnitude 1
         result *= v0xz;                                // set magnitude of xz to v0xz (starting speed in xz plane)
         result.y = v0y;                                // set y to v0y (starting speed of y plane)
     
         return result;
     
    }

}
