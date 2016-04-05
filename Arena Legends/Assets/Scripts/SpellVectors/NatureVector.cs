using UnityEngine;
using System.Collections;

public class NatureVector : SpellVector 
{
    private GameObject target;
    private GameObject spEffect;

    public NatureVector(float damage, float cost, float effect, GameObject spellEffect, GameObject tar) : base(damage, cost, effect)
    {
        this.target = tar;
        this.spEffect = spellEffect;
    }

    // SpellVector need a way to get created.
    public override void CreateSkill()
    {

    }

    // This is needed to cast the SpellVector.
    public override void Cast(float damage, float effect)
    {

    }

    // This will create the prefab of the SpellVector.
    public override void CreatePrefab()
    {

    }

	
}
