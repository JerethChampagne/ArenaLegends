using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class myCombatEvent : UnityEvent<float> 
{
}

public class PlayerController : MonoBehaviour 
{
    public float speed;
    public float impulse;
    public float hp;
    public Text countText;
    public Text winText;
    public GameObject turretPrefab;
    public GameObject explosion;
    public List<GameObject> turrets;

    private Rigidbody rb;
    public int count;

    public myCombatEvent CombatEvent;

	// Use this for initialization
	void Start () 
    {
        turrets = new List<GameObject>();
        rb = GetComponent<Rigidbody>();
        SetCountText();
        winText.text = "";
        if (CombatEvent == null) 
        {
            CombatEvent = new myCombatEvent();
        }
        CombatEvent.AddListener(TakeDamage);
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (count > 1) 
        {
            if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E))
            {
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    // Make a Turret.
                    MakeTurret();
                }
                else
                {
                    // Push enemies away.
                    PushEnemiesAway();
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.F12)) 
        {
            count += 1000;
            SetCountText();
        }

        impulse = count;
        
	}

    void FixedUpdate () 
    {
        float moveHorizantal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizantal, 0.0f, moveVertical);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(movement * speed * impulse);
        }
        else 
        { 
            rb.AddForce(movement * speed); 
        }
    }

    void OnTriggerEnter (Collider other) 
    {
        if (other.gameObject.CompareTag("Pick Up")) 
        {
            other.gameObject.SetActive(false);
            Destroy(other.gameObject, 0.1f);
            count++;
            impulse++;
            SetCountText();
            CheckCount();
        }
    }

    void SetCountText () 
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 100) 
        {
            winText.text = "You Win!";
        }

    }

    void CheckCount() 
    {
        if ((count % 25) == 0) 
        {
            GameObject.Find("EnemySpawner").BroadcastMessage("IncreaseEnemyCount", 1);
        }
    }

    void TakeDamage(float damage) 
    {
        hp -= damage;

        CheckHealth();

    }

    void CheckHealth() 
    {
        if (this.hp <= 0) 
        {
            // Player loses.
            winText.text = "YOU LOSE! HAHAHA!";
            MonoBehaviour.Destroy(this.gameObject, 10.0f);
        }
    }

    void PushEnemiesAway() 
    {
        // Get explosions point of origin.
        Vector3 explosionPos = this.transform.position;

        GameObject GO = MonoBehaviour.Instantiate(explosion) as GameObject;
        GO.transform.position = this.transform.position;

        // Get the layer mask.
        int mask = LayerMask.NameToLayer("Enemy");

        // Do an overlap sphere.
        Collider[] cols = Physics.OverlapSphere(explosionPos, 5.0f, 1 << mask);
        foreach (Collider col in cols) 
        {
            Rigidbody RB = col.GetComponent<Rigidbody>();

            if (RB != null) 
            {
                RB.AddExplosionForce(1000, explosionPos, 5.0f);
            }

            RB.gameObject.BroadcastMessage("TakeDamage", count / 5);
            SubtractCount(Mathf.CeilToInt(count / 5));
        }

        MonoBehaviour.Destroy(GO, 2.0f);
    }

    void MakeTurret() 
    {
        // Get a cost.
        SubtractCount(Mathf.CeilToInt(count / 2));

        // Instantiate a Turret.
        GameObject GO = MonoBehaviour.Instantiate(turretPrefab) as GameObject;
        turrets.Add(GO);

        GO.transform.position = this.transform.position + ( this.transform.forward * (-1) );
    }

    void Attack() 
    {
        // Get the layer mask.
        int mask = LayerMask.NameToLayer("Enemy");

        // Do an overlap sphere.
        Collider[] cols = Physics.OverlapSphere(this.transform.position, 5.0f, 1 << mask);
        foreach (Collider col in cols)
        {
            col.gameObject.BroadcastMessage("TakeDamage", count / 2);
        }
    }

    void SubtractCount(int i) 
    {
        this.count -= i;
    }

    
}
