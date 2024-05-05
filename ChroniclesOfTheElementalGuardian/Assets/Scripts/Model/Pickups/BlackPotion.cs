using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackPotion : Potion
{
    public override void Use(PlayerStats playerStats)
    {
        ActivateDuration();
    }

    protected override void DeactivateEffect()
    {
        
    }
}
