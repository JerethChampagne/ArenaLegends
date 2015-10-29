using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class EntityInfo : MonoBehaviour 
{

    public float Health, Strength, Defense, Intellect, Dexterity;
    float Thealth, Tstrength, Tdefense, Tintellect, Tdexterity;
    float Spellpower, Attack;
    float MoveSpeed, TMoveSpeed;
    int Level;
    float Exp, ExpToNextLevel;

    List<float> cooldowns;

    public GameObject Target; // This is a reference to the target that this Gameobject is currently going to attack/support.

    Entity info;

    Spellbook Spells;

	// Use this for initialization
	void Start () 
    {
        info = new Entity(100, 10, 10, 10, 10, this.gameObject);
        cooldowns = new List<float>();
	}
	
	// Update is called once per frame
	void Update () 
    {

        IncrementCooldowns(Time.deltaTime);
	
	}

    void IncrementCooldowns(float time) 
    {

        for (int i = 0; i < cooldowns.Count; i++)
        {
            cooldowns[i] += time;
        }

    }

    /// <summary>
    ///  This Should be called anytime at least one of the stats are altered.
    /// </summary>
    public void SetStats() 
    {
        SetDefense();
        SetDexterity();
        SetIntellect();
        SetStrength();
        SetMoveSpeed();
        SetSpellpower();
        SetAttack();
        SetFullHealth();
    }

    void SetFullHealth() 
    {
        this.Health = this.info.GetBaseHealth() + Thealth + (this.Defense * .5f) + (this.Strength * .25f);
    }

    void SetAttack() 
    {
        this.Attack = this.Strength + (this.Dexterity * 0.25f) + (this.Defense * 0.15f);
    }

    void SetSpellpower() 
    {
        this.Spellpower = this.Intellect * 1.5f;
    }

    void SetMoveSpeed() 
    {
        //this.MoveSpeed = 
    }

    void SetStrength() 
    {
        this.Strength = this.info.GetBaseStrength() + Tstrength;
    }

    void SetDefense() 
    {
        this.Defense = this.info.GetBaseDefense() + Tdefense;
    }

    void SetIntellect() 
    {
        this.Intellect = this.info.GetBaseIntellect() + Tintellect;
    }

    void SetDexterity() 
    {
        this.Dexterity = this.info.GetBaseDexterity() + Tdexterity;
    }

    public void CastSpell(int num) 
    {
        if ((cooldowns[num] - Spells.GetCooldown(num)) >= 0.0f) 
        {
            Spells.Cast(num, gameObject, Target);
            cooldowns[num] = 0.0f;
            return;
        }

        throw new System.Exception("Skill is on cooldown!");

    }

    public void AddSpell() 
    {
        cooldowns.Add(0.0f);
        throw new System.NotImplementedException("Adding a skill is not implemented yet!");
        
    }
 
    public void SetTarget(GameObject target) 
    {
        this.Target = target;
    }

    public void AddExperience(float amount) 
    {
        this.Exp += amount;
        if (CheckLevelUp()) 
        {
            // It is true so we need to determine to new amount of experience needed to level.
            float tempAmount = Mathf.Abs(this.Exp - this.ExpToNextLevel);
            // tempAmount is the experience the player will have when they start this level.
            this.Level++;
            this.Exp = tempAmount;
            this.ExpToNextLevel += (this.ExpToNextLevel * (.5f)) + (2 * this.Level);

        }
    }

    bool CheckLevelUp() 
    {
        if (this.Exp >= this.ExpToNextLevel) 
        {
            return true;
        }

        return false;
        
    }



}
