using UnityEngine;
using System.Collections;

public class MouseClickTarget : MonoBehaviour 
{

    public GameObject player;
    PlayerBehavior pMono;

	// Use this for initialization
	void Start () 
    {
        pMono = player.GetComponent<PlayerBehavior>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)) 
        {
            Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hitInfo;

            Physics.Raycast(r, out hitInfo, Mathf.Infinity);

            Transform GO = hitInfo.collider.gameObject.transform;

            pMono.SetTarget(GO.gameObject);
        }
	
	}
}
