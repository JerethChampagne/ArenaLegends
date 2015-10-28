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

public class SpellVector
{

    float damage, cost, effect;
    VectorType spellType;
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
    public SpellVector(float damage, float cost, float effect, VectorType type)
    {
        SpellStats = new Vector3(damage, cost, effect);
        spellType = type;
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

    public void Cancel() 
    {
        SpellStats = new Vector3();
        spellType = VectorType.None;
    }

    void CreateSkill() 
    {
        if (support)
        {
            switch (spellType)
            {
                case VectorType.Fire:
                    skill = new Skill("Fire Vector", "Fire spell that depends on the offering.", (this.damage + this.cost), 30f, 2f, 0f, null);
                    break;

                case VectorType.Ice:
                    skill = new Skill("Ice Vector", "Ice spell that depends on the offering.", (this.damage + this.cost), 30f, 2f, 0f, null);
                    break;

                case VectorType.Nature:
                    skill = new Skill("Nature Vector", "Nature spell that depends on the offering.", (this.damage + this.cost), 30f, 2f, 0f, null);
                    break;

                case VectorType.Void:
                    skill = new Skill("Void Vector", "Void spell that depends on the offering.", (this.damage + this.cost), 30f, 2f, 0f, null);
                    break;

                case VectorType.Creation:
                    skill = new Skill("Creation Vector", "Creation spell that depends on the offering.", (this.damage + this.cost), 30f, 2f, 0f, null);
                    break;

                default:

                    break;
            }
        }

        if (!support) 
        {
            switch (spellType)
            {
                case VectorType.Fire:
                    skill = new Skill("Fire Vector", "Fire spell that depends on the offering.", (this.damage - this.cost), 30f, 2f, 0f, null);
                    break;

                case VectorType.Ice:
                    skill = new Skill("Ice Vector", "Ice spell that depends on the offering.", (this.damage - this.cost), 30f, 2f, 0f, null);
                    break;

                case VectorType.Nature:
                    skill = new Skill("Nature Vector", "Nature spell that depends on the offering.", (this.damage - this.cost), 30f, 2f, 0f, null);
                    break;

                case VectorType.Void:
                    skill = new Skill("Void Vector", "Void spell that depends on the offering.", (this.damage - this.cost), 30f, 2f, 0f, null);
                    break;

                case VectorType.Creation:
                    skill = new Skill("Creation Vector", "Creation spell that depends on the offering.", (this.damage - this.cost), 30f, 2f, 0f, null);
                    break;

                default:

                    break;
            }
        }
    }

    public void Cast() 
    {

    }

    void CreatePrefab() 
    {

    }



}
