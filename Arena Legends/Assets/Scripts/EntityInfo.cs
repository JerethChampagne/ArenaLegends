using UnityEngine;
using System.Collections;

public class EntityInfo : MonoBehaviour 
{

    public float Health, Strength, Defense, Intellect, Dexterity;
    float Thealth, Tstrength, Tdefense, Tintellect, Tdexterity;
    float Spellpower, Attack;
    float MoveSpeed, TMoveSpeed;
    int Level;
    float Exp, ExpToNextLevel;

    Entity info;

    Spellbook Spells;

	// Use this for initialization
	void Start () 
    {
        info = new Entity(100, 10, 10, 10, 10, this.gameObject);
	}
	
	// Update is called once per frame
	void Update () 
    {
	
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
        Spells.Cast(num);
    }

    public void AddSpell() 
    {

    }

}
