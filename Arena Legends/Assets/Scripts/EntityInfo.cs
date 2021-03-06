﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public enum ClassType 
{
    Warrior,
    Mage,
    Rogue
}

public enum DamageType 
{
    Skill,
    Effect
}

public class EntityInfo : MonoBehaviour 
{

    public float Health, Strength, Defense, Intellect, Dexterity, MoveSpeed;
    float Thealth, Tstrength, Tdefense, Tintellect, Tdexterity, TMoveSpeed;
    public float Spellpower, Attack;
    public int Level;
    public int Exp, ExpToNextLevel;
    bool init = false;

    public bool stunned = false;
    public bool frozen = false;

    ClassType _class;

    //List<float> cooldowns;

    //public GameObject Target; // This is a reference to the target that this Gameobject is currently going to attack/support.

    Entity info;

    Spellbook Spells;

	// Use this for initialization
	void Start () 
    {
        info = new Entity(100, 10, 10, 10, 10, this.gameObject);
        SetStats();
        //cooldowns = new List<float>();
        Spells = new Spellbook();
        if (!init) 
        {
            Init();
        }
        
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (!init) 
        {
            Init();
        }



        //IncrementCooldowns(Time.deltaTime);

       /* // This is used for testing:
        if (Input.GetKeyDown(KeyCode.F12)) 
        {
            //KillEnemy();
            ThrowSomething();
        }*/

        if (this.Health <= 0) 
        {
            this.Health = 0;
            PlayDeath();
        }

	}

    void Init() 
    {
        this.Level = 1;
        this.Exp = 0;
        this.ExpToNextLevel = 10;
        init = true;
    }

    public float GetHealth()
    {
        return this.Health;
    }
    public float GetStrength() 
    {
        return this.Strength;
    }
    public float GetDefense() 
    {
        return this.Defense;
    }
    public float GetIntellect() 
    {
        return this.Intellect;
    }
    public float GetDexterity() 
    {
        return this.Dexterity;
    }
    public float GetMovespeed() 
    {
        return this.MoveSpeed;
    }
    public float GetAttack() 
    {
        return this.Attack;
    }
    public float GetSpellpower() 
    {
        return this.Spellpower;
    }

    public void ReduceHealth(float amount, DamageType type) 
    {
        
        switch (type) 
        {
            case DamageType.Effect:
                Debug.Log("Damage is " + amount);
                this.Health -= amount;
                break;
            case DamageType.Skill:
                Debug.Log("Damage before reduction is " + amount);
                amount = amount - (amount / this.Defense);
                Debug.Log("Damage after reduction is " + amount);
                this.Health -= amount;
                break;
            default:
                Debug.LogError("Function 'ReduceHealth(float amount, DamageType type)' is using default case, BAD DAMAGETYPE PARAMETER.");
                break;
        }
        
    }

    public void AlterHealthOffset(float amount) 
    {
        this.Thealth += amount;
        SetStats();
    }

    public void AlterStrength(float amount) 
    {
        this.Tstrength += amount;
        SetStats();
    }

    public void AlterDefense(float amount) 
    {
        this.Tdefense += amount;
        SetStats();
    }

    public void AlterDexterity(float amount) 
    {
        this.Tdexterity += amount;
        SetStats();
    }

    public void AlterIntellect(float amount) 
    {
        this.Tintellect += amount;
        SetStats();
    }

    public void AlterSpeed(float amount) 
    {
        this.TMoveSpeed += amount;
        SetStats();
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
        this.Health = Mathf.CeilToInt(this.info.GetBaseHealth() + Thealth + (this.Defense * .5f) + (this.Strength * .25f) + (this.Dexterity * .25f) + (this.Intellect * .25f) + (this.Health / 2));
        
    }

    void SetAttack() 
    {
        this.Attack = Mathf.CeilToInt(this.Strength + (this.Dexterity * 0.25f) + (this.Defense * 0.15f));
    }

    void SetSpellpower() 
    {
        this.Spellpower = Mathf.CeilToInt(this.Intellect * 1.75f);
    }

    void SetMoveSpeed() 
    {
        this.MoveSpeed = Mathf.CeilToInt(this.TMoveSpeed + info.GetBaseMoveSpeed());
    }

    void SetStrength() 
    {
        this.Strength = Mathf.CeilToInt(this.info.GetBaseStrength() + this.Tstrength);
    }

    void SetDefense() 
    {
        this.Defense = Mathf.CeilToInt(this.info.GetBaseDefense() + this.Tdefense);
    }

    void SetIntellect() 
    {
        this.Intellect = Mathf.CeilToInt(this.info.GetBaseIntellect() + this.Tintellect);
    }

    void SetDexterity() 
    {
        this.Dexterity = Mathf.CeilToInt(this.info.GetBaseDexterity() + this.Tdexterity);
    }

    public void CastSpell(int num, GameObject Target) 
    {
        // Check if the skill is on cooldown.
        if ( !Spells.GetCooldown(num) ) 
        {
            Spells.Cast(num, gameObject, Target);
            //cooldowns[num] = 0.0f;
            return;
        }

        // Skill did not meet one or more of the criteria to be cast.
        Debug.Log("Skill could not be used.");
        if (Spells.GetCooldown(num))
        {
            Debug.Log("Skill is on cooldown.");
        }
        else { Debug.Log("Skill could not be used for some other reason."); }

        return;

    }

    public void AddSpell(Skill sk) 
    {
        //cooldowns.Add(0.0f);
        Spells.AddSkill(sk);
        //throw new System.NotImplementedException("Adding a skill is not implemented yet!");
        
    }

    public void AddExperience(int amount) 
    {
        this.Exp += amount;
        if (CheckLevelUp()) 
        {
            // It is true so we need to determine to new amount of experience needed to level.
            int tempAmount = Mathf.Abs(this.Exp - this.ExpToNextLevel);
            // tempAmount is the experience the player will have when they start this level.
            this.Level++;
            this.Exp = tempAmount;
            this.ExpToNextLevel += Mathf.CeilToInt(this.ExpToNextLevel * (.5f) + (2 * this.Level));

            // Level up the base Entity also:
            switch (_class) 
            {
                case ClassType.Mage:
                    this.info.LevelUp(1f, 1f, 1f, 3f);
                    break;

                case ClassType.Rogue:
                    this.info.LevelUp(1f, 1f, 3f, 1f);
                    break;

                case ClassType.Warrior:
                    this.info.LevelUp(2f, 2f, 1f, 1f);
                    break;

                default:

                    break;
            }
            SetStats();
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


    void KillEnemy() 
    {
        AddExperience(1);
    }

    private void ThrowSomething() 
    {
        //GetComponentInChildren<LobObject>().Init(Target.transform.position, 1.0f);
    }

    void PlayDeath() 
    {
        // Play the Death animation here.

        Destroy(this.gameObject, 4.0f);
    }

}
