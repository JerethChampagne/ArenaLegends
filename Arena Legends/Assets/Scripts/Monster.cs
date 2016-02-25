using UnityEngine;
using System.Collections;

public class Monster : Entity
{
    protected string Name; // Name for the monster.

    // These are not needed since this class is inherited from the Entity class.
    //protected float Health, Defense, Strength, Intellect, Dexterity, MoveSpeed; 


    // Constructor function.
    Monster(string name, float hp, float def, float str, float intel, float dex, GameObject me) : base(hp, def, str, intel, dex, me)
    {
        Name = name;
      /*  Health = hp;
        Defense = def;
        Strength = str;
        Intellect = intel;
        Dexterity = dex;
        MoveSpeed = movespeed;
       */
    }

    public string GetName()
    {
        return this.Name;
    }

    




	

}
