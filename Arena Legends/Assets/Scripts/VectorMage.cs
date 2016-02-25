using UnityEngine;
using System.Collections;

public class VectorMage : Entity 
{

    SpellVector spellVector;


    public VectorMage(float hp, float str, float def, float intel, float dex, GameObject me) : base(hp, str, def, intel, dex, me)
    {
        
    }

    public void CreateVector(float damage, float cost, float effect, float offering, GameObject spellEffect, GameObject target)
    {
        // We need to get the percentage increase from cost to offering (i.e. offering = 52 and cost = 50, so increase = 52/50 = 1.04 so damage and effect increase by 1.04).
        float increase = offering / cost;

        // Create the SpellVector to cast.
        spellVector = new FireVector( (damage * increase), cost, (effect * increase), spellEffect, target);

    }

    public void CancelVector() 
    {
        spellVector.Cancel();
    }

    public void CastVector() 
    {
        // TODO: Implement CastVector.
    }

}
