using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum StatChange 
{
    Strength,
    Defense,
    Dexterity,
    Intellect,
    Speed
}

public abstract class IBuffable
{
    public abstract void Apply(EntityInfo victim);
    public float FinishTime { get; set; }
    public bool finished { get; set; }
    public int value { get; set; }
    protected bool init { get; set; }

}

public class Poison : IBuffable 
{

    public override void Apply(EntityInfo victim) 
    {
        
        if (victim != null) 
        {

            victim.ReduceHealth(value * Time.deltaTime, DamageType.Effect);
            
        }
    }
}

public class StatMod : IBuffable 
{
    List<StatChange> stats = new List<StatChange>();


    public void AddStatWatch(StatChange stat)
    {
        stats.Add(stat);
    }


    public override void Apply(EntityInfo victim) 
    {
        if (finished) 
        {
            value *= -1;
        }

        if (init && !finished) 
        {
            return;
        }

        for (int i = 0; i < stats.Count; i++) 
        {
            switch (stats[i]) 
            {
                case StatChange.Defense:
                    victim.AlterDefense(value);
                    break;

                case StatChange.Dexterity:
                    victim.AlterDexterity(value);
                    break;

                case StatChange.Strength:
                    victim.AlterStrength(value);
                    break;

                case StatChange.Intellect:
                    victim.AlterIntellect(value);
                    break;

                case StatChange.Speed:
                    victim.AlterSpeed(value);
                    break;

                default:

                    break;
            }
        }
        init = true;
        return;
    }
 

}

public class Burn : IBuffable
{

    public override void Apply(EntityInfo victim) 
    {
        if (victim != null) 
        {
            victim.ReduceHealth(value * Time.deltaTime, DamageType.Effect);
        }
    }
}

public class Stun : IBuffable 
{

    public override void Apply(EntityInfo victim) 
    {
        if (victim != null) 
        {

            if (this.FinishTime >= Time.time) 
            {
                victim.stunned = false;
                return;
            }

            victim.stunned = true;

        }
    }
}

public class Freeze : IBuffable 
{

    public override void Apply(EntityInfo victim) 
    {
        if (victim != null)
        {

            if (this.FinishTime >= Time.time)
            {
                victim.frozen = false;
                return;
            }

            victim.frozen = true;

        }
    }
}

public class ExtraHits : IBuffable 
{
    public int HitNum { get; set; }

    public override void Apply(EntityInfo victim)
    {
        victim.ReduceHealth(value, DamageType.Effect);
        HitNum--;

        if (HitNum <= 0) 
        {
            finished = true;

            return;
        }

        return;

    }
}

