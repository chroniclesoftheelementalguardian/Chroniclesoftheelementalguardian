using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhitePotion : Potion
{
    public override void Use(PlayerStats playerStats)
    {
        _playerStats = playerStats;
        ActivateEffect();
        ActivateDuration();
        DeactivateVisuals();
        SpawnEffectText("Move Speed++");
    }

    protected override void ActivateEffect()
    {
        _increaseAmount = _playerStats.MoveSpeed * (_increasePercentage / 100);
        _playerStats.MoveSpeed += _increaseAmount;
    }

    protected override void DeactivateEffect()
    {
        _playerStats.MoveSpeed -= _increaseAmount;
    }
}
