using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class BuffManager : MonoBehaviour 
{

    public bool poison, statmod, frozen, burn, stun, extrahits;

    List<IBuffable> buffables = new List<IBuffable>();
    EntityInfo eInfo;

    void Awake() 
    {
        if (eInfo == null) 
        {
            // Make a reference to the EntityInfo of this Buffmanager's specific character.
            eInfo = GetComponent<EntityInfo>();
        }

    }

	// Use this for initialization
	void Start () 
    {
	    
	}
	
	// Update is called once per frame
	void Update () 
    {

        for (int i = 0; i < buffables.Count; i++) 
        {
            buffables[i].Apply(this.eInfo);

            // Check if the buff is finished.
            if (Time.time >= buffables[i].FinishTime) 
            {
                if (buffables[i] is StatMod) 
                {
                    statmod = true;
                    // Buff is a StatMod, so we need to undo its effects and then remove it.
                    buffables[i].finished = true;
                    buffables[i].Apply(this.eInfo);
                }
                if (buffables[i] is Burn) 
                {
                    burn = true;
                }
                if (buffables[i] is Stun) 
                {
                    stun = true;
                }
                if (buffables[i] is Poison) 
                {
                    poison = true;
                }
                if (buffables[i] is Freeze) 
                {
                    frozen = true;
                }
                if (buffables[i] is ExtraHits) 
                {
                    extrahits = true;
                }

                // Remove this buff.
                buffables.Remove(buffables[i]); 
            }
        }
	    
	}

    public void AddBuffable(IBuffable buff, float timer)
    {
        // Setup when the buff should finish.(current time + timer)
        buff.FinishTime = Time.time + timer;
        buffables.Add(buff);
    }

    public void AddBuffable(IBuffable buff, float timer, int value) 
    {
        // Setup when the buff should finish
        buff.FinishTime = Time.time + timer;
        buff.value = value;
        buffables.Add(buff);
    }

}
