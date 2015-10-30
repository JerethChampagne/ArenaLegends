using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spellbook
{
    List<Skill> Skills; 

    public Spellbook() 
    {
        Skills = new List<Skill>();
    }

    public void AddSkill(Skill sk) 
    {
        Skills.Add(sk);
    }

    public void Cast(int num, GameObject caster, GameObject target) 
    {
        Skills[num].Cast(target, caster);
    }

    public float GetCooldown(int num)
    {
        return Skills[num].GetCooldown();
    }

    public Skill GetSkill(int i)
    {
        return Skills[i];
    }

}
