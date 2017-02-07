using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class PlayerShooting : MonoBehaviour
{
    public int damagePerShot = 20;
    public float timeBetweenBullets = 0.15f;
    public float range = 100f;
    public float fuel = 100f;
    public bool canFlame = true;
    public float audioTime;
    public float waitTime;
    public GameObject flamethrower;
    public float FlameCost;
    public float FlameDPS;
    public float fuelRegenTimer;
    public float flameWaitTime;
    public Slider fuelSlider;
    

    float timer;
    Ray shootRay = new Ray();
    RaycastHit shootHit;
    int shootableMask;
    ParticleSystem gunParticles;
    LineRenderer gunLine;
    AudioSource gunAudio;
    Light gunLight;
    float effectsDisplayTime = 0.2f;


    void Awake ()
    {
        shootableMask = LayerMask.GetMask ("Shootable");
        gunParticles = GetComponent<ParticleSystem> ();
        flamethrower = GameObject.Find("Flamethrower");
        StopFlame();
        gunLine = GetComponent <LineRenderer> ();
        gunAudio = GetComponent<AudioSource> ();
        gunLight = GetComponent<Light> ();

    }


    void Update ()
    {
        timer += Time.deltaTime;
        
		if(Input.GetButton ("Fire1") && !Input.GetButton("Fire2") && timer >= timeBetweenBullets && Time.timeScale != 0)
        {
            Shoot ();
        }
        if (CanFlame() && !Input.GetButton("Fire1") && Input.GetButton("Fire2"))
        {
            Flame();
        }
        else 
        {
            StopFlame();
        }

        if(timer >= timeBetweenBullets * effectsDisplayTime)
        {
            DisableEffects ();
        }

        fuelSlider.value = fuel;

        
    }


    public void DisableEffects ()
    {
        gunLine.enabled = false;
        gunLight.enabled = false;
    }


    void Shoot ()
    {
        timer = 0f;

        gunAudio.Play ();

        gunLight.enabled = true;

        gunParticles.Stop ();
        gunParticles.Play ();

        gunLine.enabled = true;
        gunLine.SetPosition (0, transform.position);

        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;

        if(Physics.Raycast (shootRay, out shootHit, range, shootableMask))
        {
            EnemyHealth enemyHealth = shootHit.collider.GetComponent <EnemyHealth> ();
            if(enemyHealth != null)
            {
                enemyHealth.TakeDamage (damagePerShot, shootHit.point);
            }
            gunLine.SetPosition (1, shootHit.point);
        }
        else
        {
            gunLine.SetPosition (1, shootRay.origin + shootRay.direction * range);
        }
    }

    void Flame() 
    {
        fuel -= FlameCost * Time.deltaTime;


        // Play audio clip.
        AudioSource audio = flamethrower.gameObject.GetComponent<AudioSource>();

        if (audio != null && audioTime <= Time.time) 
        {
            audio.Play();
            audioTime = Time.time + waitTime;
        }

        // Play particle system if it is not playing.
        foreach (Transform ps in flamethrower.transform) 
        {
            ps.gameObject.GetComponent<ParticleSystem>().Play();
        }
        

        Collider[] cols = Physics.OverlapSphere(transform.position, 6f, shootableMask);
        
        foreach (Collider col in cols) 
        {
            if (col.CompareTag("Enemy")) 
            {
                float angle = Vector3.Angle((col.transform.position - transform.position), transform.forward);

                // Do damage.
                if (angle <= 25f)
                {
                    EnemyHealth enemyHealth = col.GetComponent<EnemyHealth>();
                    enemyHealth.TakeDamage(Mathf.CeilToInt(FlameDPS * Time.deltaTime));
                }
            }
            
        }

    }

    void StopFlame() 
    {
        foreach (Transform ps in flamethrower.transform)
        {
            ps.gameObject.GetComponent<ParticleSystem>().Stop();
        }
    }

    bool CanFlame() 
    {
        // The flamethrower is not on cooldown.
        if (fuelRegenTimer <= Time.time)
        {
            // No more fuel.
            if (fuel < 0)
            {
                // Put flamethrower on cooldown.
                fuel = 0;
                fuelRegenTimer = Time.time + flameWaitTime;
                return false;
            }
            else if (fuel == 0)
            {
                fuel = 100f;
                // There is fuel.
                return true;
            }
            else
            {
                return true;
            }


        }
        else
        {
            // This statement is probably not needed, but let us be safe.
            
            // Else is if fuelRegenTimer > Time.time; so the flamethrower is on cooldown.
            return false;
        }
        
    }
}
