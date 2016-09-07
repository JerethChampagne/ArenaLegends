using UnityEngine;
using System.Collections;

public class PlayerBehavior : MonoBehaviour 
{

    EntityInfo eInfo;
    public GameObject target;
    bool init = false;

    // This is for testing the particle systems.
    public PsysManager psys;

	// Use this for initialization
	void Start () 
    {
        psys = GameObject.Find("Spell Cast Transform").GetComponent<PsysManager>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (!init) 
        {
            Init();
        }

        Move();

        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            Debug.Log("Adding Firebolt to the spellbook.");
            eInfo.AddSpell(new Skill("Firebolt", "Lobs a bolt of fire.", 10f, 15f, 5f, 90f, psys.GetSystem("Firebolt").psys ));
            Debug.Log("Firebolt has been added to the spellbook.");
            
            Debug.Log("Adding Ice Spikes to the spellbook.");
            eInfo.AddSpell(new Skill("Ice Spikes", "Ice spikes chase after an enemy target.", 10f, 15f, 5f, 90f, psys.GetSystem("IceSpikes").psys));
            Debug.Log("Ice Spikes has been added to the spellbook.");
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            eInfo.CastSpell(1, this.target);
        }
	
	}

    void Init() 
    {
        eInfo = gameObject.GetComponent<EntityInfo>();
    }

    public void SetTarget(GameObject _target)
    {
        this.target = _target;
    }

    void Move() 
    {
        // If the player is pressing at least one of the WASD keys.
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)) 
        {
            // Player is pressing 'W'
            if (Input.GetKey(KeyCode.W)) 
            {
                transform.position += transform.forward * eInfo.GetMovespeed() * Time.deltaTime;
                // Player is trying to go forward and left.
                if (Input.GetKey(KeyCode.A)) 
                {
                    transform.position -= transform.right * eInfo.GetMovespeed() * Time.deltaTime;
                }
                // Player is trying to go forward and right.
                if (Input.GetKey(KeyCode.D)) 
                {
                    transform.position += transform.right * eInfo.GetMovespeed() * Time.deltaTime;
                }
                // Player is trying to go forward and back, cancelling each other.
                if (Input.GetKey(KeyCode.S)) 
                {
                    transform.position -= transform.forward * eInfo.GetMovespeed() * Time.deltaTime;
                }

                return;
            }
            // Player is pressing 'A'
            if (Input.GetKey(KeyCode.A))
            {
                transform.position -= transform.right * eInfo.GetMovespeed() * Time.deltaTime;
                if (Input.GetKey(KeyCode.W))
                {
                    transform.position += transform.forward * eInfo.GetMovespeed() * Time.deltaTime;
                }
                // Player is trying to go left and right, cancelling each other.
                if (Input.GetKey(KeyCode.D))
                {
                    transform.position += transform.right * eInfo.GetMovespeed() * Time.deltaTime;
                }
                // Player is trying to go forward and back.
                if (Input.GetKey(KeyCode.S))
                {
                    transform.position -= transform.forward * eInfo.GetMovespeed() * Time.deltaTime;
                }

                return;
            }
            // Player is pressing 'D'
            if (Input.GetKey(KeyCode.D))
            {
                transform.position += transform.right * eInfo.GetMovespeed() * Time.deltaTime;
                // Player is trying to go right and left, cancelling each other.
                if (Input.GetKey(KeyCode.A))
                {
                    transform.position -= transform.right * eInfo.GetMovespeed() * Time.deltaTime;
                }
                // Player is trying to go forward and right.
                if (Input.GetKey(KeyCode.W))
                {
                    transform.position += transform.forward * eInfo.GetMovespeed() * Time.deltaTime;
                }
                // Player is trying to go right and back.
                if (Input.GetKey(KeyCode.S))
                {
                    transform.position -= transform.forward * eInfo.GetMovespeed() * Time.deltaTime;
                }

                return;
            }
            // Player is pressing 'S'
            if (Input.GetKey(KeyCode.S))
            {
                transform.position -= transform.forward * eInfo.GetMovespeed() * Time.deltaTime;
                // Player is trying to go back and left.
                if (Input.GetKey(KeyCode.A))
                {
                    transform.position -= transform.right * eInfo.GetMovespeed() * Time.deltaTime;
                }
                // Player is trying to go back and right.
                if (Input.GetKey(KeyCode.D))
                {
                    transform.position += transform.right * eInfo.GetMovespeed() * Time.deltaTime;
                }
                // Player is trying to go forward and back, cancelling each other.
                if (Input.GetKey(KeyCode.W))
                {
                    transform.position += transform.forward * eInfo.GetMovespeed() * Time.deltaTime;
                }

                return;
            }

        }
    }


}
