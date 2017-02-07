using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public enum EnemyState 
{
    Waiting,
    Wandering,
    PlayerFound,
    Pursuit
}

public class EnemyBehaviour : MonoBehaviour 
{
    public float speed;
    public bool init = false;
    public float hp;
    public GameObject player;
    public float waitTimer;
    public float finishTime;
    public Vector3 endPoint;

    public myCombatEvent CombatEvent;

    public EnemyState currentState;

    private Rigidbody rb;

	// Use this for initialization
	void Start () 
    {
        if (CombatEvent == null) 
        {
            CombatEvent = new myCombatEvent();
        }

        CombatEvent.AddListener(TakeDamage);
	}
	
	// Update is called once per frame
	void Update () 
    {
        // Initialize the data if it has not been done yet.
        if (!init) 
        {
            Initialize();
            return;
        }

        switch (currentState) 
        {
            case EnemyState.Waiting:
                if (waitTimer < finishTime)
                {
                    // Increment the waitTimer.
                    waitTimer += Time.deltaTime;
                }
                else 
                {
                    // Find a new point to wander towards.
                    FindAWanderPoint();
                    currentState = EnemyState.Wandering;
                }
                break;
            case EnemyState.Wandering:
                
                if (Vector3.Distance(endPoint, this.transform.position) > 1)
                {
                    // Have not reached the point yet. Move towards it.
                    Wander();
                }
                else 
                {
                    // We have reached the point. We must wait now.
                    finishTime = waitTimer + Random.Range(3, 11);
                    // Put enemy back into the Waiting state.
                    currentState = EnemyState.Waiting;
                }
                break;
            case EnemyState.PlayerFound:
                // Keeping this state here in case more actions are needed to be done.

                // For now, go straight to Pursuit state.
                currentState = EnemyState.Pursuit;
                break;
            case EnemyState.Pursuit:
                Move();
                break;
            default: 
                break;
        }
	}

    void Initialize() 
    {
        
        // Reference the Rigidbody.
        rb = GetComponent<Rigidbody>();

        // Make the enemy start in the waiting state.
        currentState = EnemyState.Waiting;

        // Get the layer mask of the Enemy layer.
        int mask = LayerMask.NameToLayer("Player");

        // Use an overlap sphere to find if there are any players around this enemy's starting point.
        Collider[] cols = Physics.OverlapSphere(this.transform.position, 10f, 1 << mask);

        foreach (Collider col in cols) 
        {
            if (col.gameObject.CompareTag("Player")) 
            {
                player = col.gameObject;
                currentState = EnemyState.PlayerFound;
            }
        }

        init = true;
    }

    void Move() 
    {
        // Direction vector.
        Vector3 dir = player.transform.position - this.transform.position;

        // Travel in that direction.
        //transform.position += dir.normalized * speed * Time.deltaTime;
        rb.AddForce(dir.normalized * speed);
    }

    void Wander() 
    {
        // Direction vector.
        Vector3 dir = endPoint - this.transform.position;

        // Travel in that direction.
        rb.AddForce(dir.normalized * speed);
        //transform.position += dir.normalized * speed * Time.deltaTime;
    }

    void FindAWanderPoint() 
    {
        float xPoint = Random.Range(-99, 100);
        float zPoint = Random.Range(-99, 100);
        this.endPoint = new Vector3(xPoint, 0.5f, zPoint);
        
    }

    void TakeDamage(float damage) 
    {
        this.hp -= damage;
        CheckHealth();
    }

    void CheckHealth()
    {
        if(hp <= 0)
        {
            MonoBehaviour.Destroy(this.gameObject, 0.5f);
        }
    }

    void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Player")) 
        {
            player = other.gameObject;
            currentState = EnemyState.Pursuit;
        }
    }

    void OnCollisionEnter(Collision col) 
    {
        if (col.collider.CompareTag("Player")) 
        {
            // Trigger the TakeDamage event on the player.
            col.collider.gameObject.BroadcastMessage("TakeDamage", 10.0f);

            // Destroy this enemy.
            MonoBehaviour.Destroy(this.gameObject);
        }
        
        
    }

}
