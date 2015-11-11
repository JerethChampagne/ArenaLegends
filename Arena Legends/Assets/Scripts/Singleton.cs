using UnityEngine;
using System.Collections;

/*
 * Coder: Jereth Champagne
 * NOTE: This code was written by using the Unity3D wiki page about Singletons. This code is not my own.
 */
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{

    private static T instance;
    private static object _lock = new object();

    public static T Instance 
    {
        get 
        {
            if (applicationIsQuitting) 
            {
                Debug.LogWarning("[Singleton] Instance '" + typeof(T) + "' already destoyed on application quit." + " Won't create again - returning null.");
                return null; 
            }

            lock (_lock) 
            {
                if (instance == null) 
                {
                    instance = (T) FindObjectOfType(typeof(T));

                    if (FindObjectsOfType(typeof(T)).Length > 1) 
                    {
                        Debug.LogError("[Singleton] Something went really wrong " + " - there should never be more than 1 singleton!" + " Reopening the scene may fix it.");
                        return instance;
                    }

                    if (instance == null) 
                    {
                        GameObject singleton = new GameObject();
                        instance = singleton.AddComponent<T>();
                        singleton.name = "(singleton) " + typeof(T).ToString();

                        DontDestroyOnLoad(singleton);

                        Debug.Log("[Singleton] An instance of " + typeof(T) +
                            " is needed in the scene, so '" + singleton +
                        "' was created with DontDestroyOnLoad.");
                    }
                    else 
                    {
                        Debug.Log("[Singelton] Using instance already created: " + instance.gameObject.name);
                    }
                }
            }

            return instance;
        }

    }

    private static bool applicationIsQuitting = false;


    public void OnDestroy() 
    {
        applicationIsQuitting = true;
    }

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

}
