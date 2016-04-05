using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spellbook
{
    List<Skill> Skills;
    List<Skill> onCooldown;

    public Spellbook() 
    {
        Skills = new List<Skill>();
        onCooldown = new List<Skill>();
    }

    public void AddSkill(Skill sk) 
    {
        Skills.Add(sk);
    }

    public void Cast(int num, GameObject caster, GameObject target) 
    {
        // Make sure the skill is not on cooldown and the target is in range of the skill.
        if ( !GetCooldown(num) && Skills[num].Cast(target, caster) ) 
        {
            PutOnCooldown(Skills[num]);

            return;

        }

        // Skill did not meet one or more of the criteria to be cast.
        Debug.Log("Skill could not be used.");
        if ( !onCooldown.Contains(Skills[num]) ) 
        {
            Debug.Log("Skill is on cooldown.");
        }
        else { Debug.Log("Skill could not be used for some other reason."); }

        return;

    }

    public bool GetCooldown(int num)
    {
        if (Skills[num].isOnCooldown())
        {
            return true;
        }
        else 
        {
            onCooldown.Remove(Skills[num]);
            return false;
        }
    }

    public Skill GetSkill(int i)
    {
        return Skills[i];
    }

    public void PutOnCooldown(Skill sk) 
    {
        if (!onCooldown.Contains(sk))
        {
            onCooldown.Add(sk);
        }
    }

}
