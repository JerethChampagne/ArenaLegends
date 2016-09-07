using UnityEngine;
using System.Collections;


[System.Serializable]
public struct Psys
{
    public string Name;
    public GameObject psys;
}

public class PsysManager : MonoBehaviour 
{
    

    public Psys[] systems;

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    public Psys GetSystem(string name) 
    {
        foreach(Psys sys in systems)
        {
            if(sys.Name == name)
            {
                return sys;
            }
        }

        return systems[0];
    }
}
