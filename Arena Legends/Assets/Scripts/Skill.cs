﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Skill
{
    string Name, Description;
    float damage, range, cooldown, angle;

    public Skill(string name, string des, float d, float r, float cd, float angle) 
    {
        this.Name = name;
        this.Description = des;
        this.damage = d;
        this.range = r;
        this.cooldown = cd;
        this.angle = angle;
    }

    public void Cast() 
    {
        
    }
    
    // Below are the Functions to find enemies that are within range of an attack.

    

}
