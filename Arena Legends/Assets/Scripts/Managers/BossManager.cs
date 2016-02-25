using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BossManager : Singleton<BossManager> 
{
    List<GameObject> bosses 
    {
        get 
        {
            return bosses;
        }
    }


    protected BossManager() { }

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}
}
