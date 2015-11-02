using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum StatChange 
{
    Strength,
    Defense,
    Dexterity,
    Intellect
}

public interface IBuffable
{
    void Apply(EntityInfo victim);
    float FinishTime { get; set; }
    bool finished { get; set; }
    int value { get; set; }
	
}

public class Poison : IBuffable 
{
    public int value { get; set; }
    public float FinishTime { get; set; }
    public bool finished { get; set; }

    public void Apply(EntityInfo victim) 
    {
        
        if (victim != null) 
        {
            victim.ReduceHealth(value);
        }
    }
}

public class StatMod : IBuffable 
{
    public int value { get; set; }
    public float FinishTime { get; set; }
    bool init = false;
    List<StatChange> stats = new List<StatChange>();
    public bool finished { get; set; }

    public void AddStatWatch(StatChange stat)
    {
        stats.Add(stat);
    }


    public void Apply(EntityInfo victim) 
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

                default:

                    break;
            }
        }
        init = true;
        return;
    }
 

}


