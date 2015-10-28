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
    }

    void Init(GameObject caster) 
    {
        this.Caster = caster;
        spellStart = Caster.transform.FindChild("Spell Cast Transform").transform.position;
        hasCaster = true;
    }

    public float GetCooldown() 
    {
        return cooldown;
    }

    public void Cast(GameObject target, GameObject caster) 
    {
        if (CastDisplacement(caster, target)) 
        {
            if (!hasCaster) 
            {
                Init(caster);
            }

            PlaySpell(target);
        }
    }

    bool CastDisplacement(GameObject caster, GameObject target) 
    {
        // Check if the skill being used is against a target that is in front of the attacker.

        // Get the vector from the defender to the attacker.
        Vector3 displacement = target.transform.position - caster.transform.position;

        // If the angle between the caster's forward and the displacement is more than 90 then the defender is behind the attacker.
        if (Vector3.Angle(caster.transform.forward, displacement) > 90) 
        {
            return false;
        }

        // Now check if the target is within range of the skill.
        if (Vector3.Distance(caster.transform.position, target.transform.position) > range) 
        {
            return false;
        }

        // The target must be in front of the caster, so this check has passed.
        return true;

    }
    
    // Below are the Functions to find enemies that are within range of an attack.

    void PlaySpell(GameObject target) 
    {
        MonoBehaviour.Instantiate(pSys);
    }


}
