using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class myGoEvent : UnityEvent<GameObject>
{ 
}

public class mySpawnEvent : UnityEvent<int> 
{
}

public class Spawner : MonoBehaviour 
{
    public GameObject Prefab;
    public int maxCount;
    public int count;
    public float Xmax;
    public float Zmax;

    public List<GameObject> prefabs;

    public float spawnTimer; // Once this timer hits 0 or lower then it spawns a new enemy.
    public float spawnCooldown; // Time between spawns, unless count reaches zero first.

    public myGoEvent GoEvent;
    public mySpawnEvent SpawnEvent;

	// Use this for initialization
	void Start () 
    {
        if (GoEvent == null || SpawnEvent == null) 
        {
            GoEvent = new myGoEvent();
            SpawnEvent = new mySpawnEvent();
        }
        GoEvent.AddListener(InstanceDestroyed);
        SpawnEvent.AddListener(IncreaseSpawnCount);

        prefabs = new List<GameObject>();
        GetSpawnArea();
        spawnTimer = spawnCooldown;
	}
	
	// Update is called once per frame
	void Update () 
    {
        // Check to see if the amount of spawns is already reached max.
        if (count != maxCount) 
        {   // Decrement the spawn timer.
            spawnTimer -= Time.deltaTime;
            // Check to see if it time to spawn a new instance.
            CheckTimer();
        }
        
	}

    void GetSpawnArea() 
    {
        // Get distance from origin to the North wall.
        GameObject wall = GameObject.Find("North Wall");
        Vector3 pos = wall.transform.position - Vector3.zero;
        Zmax = pos.z;
        wall = GameObject.Find("East Wall");
        pos = wall.transform.position - Vector3.zero;
        Xmax = pos.x;
    }

    void CheckTimer() 
    {
        if (spawnTimer <= 0) 
        {
            // Spawn a new instance.
            CreateInstance();

            // Increment count.
            count++;

            // Reset the timer.
            spawnTimer = spawnCooldown;
        }
    }

    void CreateInstance() 
    {
        // Create an instance.
        GameObject GO = MonoBehaviour.Instantiate(Prefab) as GameObject;

        // Give it a random position within the platform.
        GivePosition(GO);

        // Add the new instance to the list.
        prefabs.Add(GO);
    }

    void GivePosition(GameObject GO) 
    {
        // Get a random X value.
        int x = Random.Range(-99, 100);

        // Get a random Z value.
        int z = Random.Range(-99, 100);

        // Set the new position.
        GO.transform.position = new Vector3(x, .5f, z);
    }

    void InstanceDestroyed(GameObject GO)
    {
        count--;
    }

    void IncreaseSpawnCount(int i) 
    {
        maxCount++;
    }
}
