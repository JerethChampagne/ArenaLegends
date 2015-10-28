using UnityEngine;
using System.Collections;

public class VectorMage : Entity 
{

    SpellVector spellVector;


    public VectorMage(float hp, float str, float def, float intel, float dex, GameObject me) : base(hp, str, def, intel, dex, me)
    {
        
    }

    public void CreateVector(float damage, float cost, float effect, VectorType type)
    {
        spellVector = new SpellVector(damage, cost, effect, type);
    }

    public void CancelVector() 
    {
        spellVector.Cancel();
    }

    public void CastVector() 
    {

    }

}
