using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum VectorType 
{
    None,
    Fire,
    Ice,
    Void,
    Nature,
    Creation
}

public abstract class SpellVector
{

    float damage, cost, effect;
    //VectorType spellType;
    Vector3 SpellStats;

    bool offensive, support;

    Skill skill;
    

    /// <summary>
    /// Creates a SpellVector object where damage is positive to do support, negative to be offensive; effect is the added effect (i.e. DoT) that the spell will have; positive means buff and negative means debuff.
    /// </summary>
    /// <param name="damage"></param>
    /// <param name="cost"></param>
    /// <param name="effect"></param>
    /// <param name="type"></param>
    public SpellVector(float damage, float cost, float effect)
    {
        SpellStats = new Vector3(damage, cost, effect);
        //spellType = type;
        if (damage < 0f) 
        {
            offensive = true;
            support = false;
        }

        else 
        {
            offensive = false;
            support = true;
        }

    }
    

    // SpellVector needs to be able to get cancelled.
    public void Cancel() 
    {
        SpellStats = Vector3.zero;
        skill = null;
        offensive = false;
        support = false;
        damage = 0f;
        cost = 0f;
        effect = 0f;

    }

    // SpellVector need a way to get created.
    public abstract void CreateSkill();

    // This is needed to cast the SpellVector.
    public abstract void Cast(float damage, float effect);

    // This will create the prefab of the SpellVector.
    public abstract void CreatePrefab();
    



}
