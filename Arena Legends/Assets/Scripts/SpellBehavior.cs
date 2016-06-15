using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpellBehavior : MonoBehaviour 
{
    bool init = false;
    bool collided = false;
    float DistanceToTarget;
    GameObject Target;
    public Vector3 endPoint;
    public float moveSpeed;
    public float yDelta;

	// Use this for initialization
	void Start () 
    {
        
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (!this.init)
            return;

        if (this.collided == false) 
        {
            // Have not collided, lets determine the direction.
            // final - initial = direction
            Vector3 direction = endPoint - this.transform.position;
            direction = direction.normalized;
            // Move in the new direction.
            Move(direction);
        }

        if (this.collided == true) 
        {
            // Once this collides with something lets delay the destroy of this gameObject.
            Destroy(this.gameObject, .5f);
        }
	}

    public void Init()
    {
        this.init = true;
        this.collided = false;
    }

    void Reset() 
    {
        // TODO: implement.
    }

    public void SetUp(GameObject target) 
    {
        this.Target = target;
        this.endPoint = target.transform.position;
    }

    void Move(Vector3 direction) 
    {
        this.transform.position += moveSpeed * Time.deltaTime * direction;
    }


}
