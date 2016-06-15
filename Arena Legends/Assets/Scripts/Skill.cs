using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Skill
{
    protected string Name, Description;
    protected float damage, range, cooldown, angle;
    protected GameObject pSys, Caster;
    protected Vector3 spellStart;
    protected bool hasCaster;
    protected float coolTimer;

    public Skill(string name, string des, float d, float r, float cd, float angle, GameObject ps) 
    {
        this.Name = name;
        this.Description = des;
        this.damage = d;
        this.range = r;
        this.cooldown = cd;
        this.angle = angle;
        this.pSys = ps;
        hasCaster = false;
        this.coolTimer = 0f;
    }

    void Init(GameObject caster) 
    {
        this.Caster = caster;
        spellStart = Caster.transform.FindChild("Spell Cast Transform").transform.position;
        hasCaster = true;
    }

    public bool isOnCooldown() 
    {

        if (coolTimer <= Time.time) 
        {
            // The coolTimer is lower then the current time implying the cooldown is finished.
            return false;
        }
        else { return true; }
        
    }

    public bool Cast(GameObject target, GameObject caster) 
    {
        // Make sure the Skill can be used on the target.
        if (CastDisplacement(caster, target)) 
        {
            coolTimer = Time.time + cooldown; // Set the next time the skill can be used.
            if (!hasCaster) 
            {
                Init(caster);
            }

            Debug.Log("There are no particle systems to play yet!");
            //PlaySpell(target);

            // Alter the Damage of the spell.
            EntityInfo pInfo = caster.GetComponent<EntityInfo>();
            float newDmg = this.damage + pInfo.GetAttack() + pInfo.GetSpellpower();

            // Apply the new damage to the target.
            EntityInfo eInfo = target.gameObject.GetComponent<EntityInfo>();
            eInfo.ReduceHealth(newDmg, DamageType.Skill);

            return true;
        }

        return false;

    }

    bool CastDisplacement(GameObject caster, GameObject target) 
    {
        // Check if the skill being used is against a target that is in front of the attacker.

        // Get the vector from the defender to the attacker.
        Vector3 displacement = target.transform.position - caster.transform.position;
        Debug.Log("Displacement Vector: " + displacement.ToString());
        // If the angle between the caster's forward and the displacement is more than 90 then the defender is behind the attacker.
        if (Vector3.Angle(caster.transform.forward, displacement) > 90) 
        {
            Debug.Log("Target is not in front of attacker.");
            return false;
        }

        // Now check if the target is within range of the skill.
        if (Vector3.Distance(caster.transform.position, target.transform.position) > range) 
        {
            Debug.Log("Target is not within the range of the skill.");
            return false;
        }

        // The target must be in front of the caster, so this check has passed.
        Debug.Log("Target is valid.");
        return true;

    }
    
    // Below are the Functions to find enemies that are within range of an attack.

    void PlaySpell(GameObject target) 
    {
        MonoBehaviour.Instantiate(pSys);

        // EffectSetting's target needs to be assigned here.

    }


}
