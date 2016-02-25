using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Entity
{
    protected float Health, Defense, Strength, Intellect, Dexterity, MoveSpeed; // All the stats for this class.
    protected GameObject prefab; // Prefab of the model used for the instance.

    protected int Level;

    //protected float exp, expToLevel;

    public Entity(float hp, float str, float def, float intel, float dex, GameObject me) 
    {
        this.Health = hp; 
        this.Strength = str;
        this.Defense = def;
        this.Intellect = intel;
        this.Dexterity = dex;
        this.prefab = me;
    }

    public float GetBaseHealth() 
    {
        return this.Health;
    }

    public float GetBaseStrength() 
    {
        return this.Strength;
    }

    public float GetBaseDefense() 
    {
        return this.Defense;
    }

    public float GetBaseIntellect() 
    {
        return this.Intellect;
    }

    public float GetBaseDexterity() 
    {
        return this.Dexterity;
    }

    public float GetBaseMoveSpeed() 
    {
        return this.MoveSpeed;
    }

    public void AddStrengthPoint(float num)
    {
        this.Strength += num;
    }

    public void AddDefensePoint(float num) 
    {
        this.Defense += num;
    }

    public void AddIntellectPoint(float num) 
    {
        this.Intellect += num;
    }

    public void AddDexterityPoint(float num) 
    {
        this.Dexterity += num;
    }

    public void LevelUp(float str, float def, float dex, float intel) 
    {
        AddDefensePoint(def);
        AddDexterityPoint(dex);
        AddIntellectPoint(intel);
        AddStrengthPoint(str);
        this.Level++;
    }

    public GameObject GetGameObject() 
    {
        return this.prefab;
    }

}
