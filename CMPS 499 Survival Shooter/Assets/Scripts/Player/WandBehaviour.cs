using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WandBehaviour : MonoBehaviour 
{
    struct Vectornian 
    {
        public Vector3 position;
        public Quaternion rotation;
    }
    public float angle1;
    public float angle2;
    public Transform O;
    Vectornian T;
    public bool hasObject = false;

	// Use this for initialization
	void Start () 
    {
		
	}
	
	// Update is called once per frame
	void Update () 
    {
        angle1 = (Input.mousePosition.x / Screen.width);
        angle2 = (Input.mousePosition.y / Screen.height);

        SetOrientation();

        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            if (!hasObject)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.forward, out hit, 100f))
                {
                    O = hit.collider.transform;

                    // Inverse S 
                    Vectornian V;
                    V.position = (-(Quaternion.Inverse(transform.rotation) * transform.position));
                    V.rotation = (Quaternion.Inverse(transform.rotation));

                    // Composition:
                    T.position = (V.position + V.rotation * O.position);
                    T.rotation = (O.rotation * V.rotation);
                    hasObject = true;

                }
            }
            else 
            {
                hasObject = false;
            }
        }

        if (hasObject) 
        {
            // O = S * T
            O.position = transform.position + transform.rotation * T.position;
            O.rotation = transform.rotation * T.rotation;
        }



	}


    void SetOrientation() 
    {
        transform.localEulerAngles = new Vector3((-75f * (angle2 - 0.5f)), (75f * (angle1 - 0.5f)), 0f);
        
    }

}
