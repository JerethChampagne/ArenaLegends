using UnityEngine;
using System.Collections;

public class MouseClickTarget : MonoBehaviour 
{

    public GameObject player;
    EntityInfo info;

	// Use this for initialization
	void Start () 
    {
        info = player.GetComponent<EntityInfo>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)) 
        {
            Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hitInfo;

            Physics.Raycast(r, out hitInfo, Mathf.Infinity);

            info.SetTarget(hitInfo.collider.gameObject);
        }
	
	}
}
