using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum Role 
{
    Tank,
    DamageDealer,
    CrowdControl,
    Support
}

public enum BossSpecial 
{
    FocusTarget,
    AoE,
    Spawner
}

public class Boss : Entity 
{
    

    public Boss(float hp, float str, float def, float intel, float dex, GameObject me, BossSpecial bs) : base(hp, str, def, intel, dex, me)
    {
        

    }

    public void SetLevel(int lvl)
    {
        while (this.Level != lvl) 
        {
            LevelUp();
        }
    }

    private void LevelUp() 
    {

    }


}
