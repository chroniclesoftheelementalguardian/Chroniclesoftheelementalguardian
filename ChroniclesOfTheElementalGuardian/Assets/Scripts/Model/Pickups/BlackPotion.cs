using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackPotion : Potion
{
    private float _manaIncreaseAmount;
    private float _healthIncreaseAmount;
    private float _moveSpeedIncreaseAmount;
    private float _physicalPowerIncreaseAmount;
    private float _abilityPowerIncreaseAmount;
    private float _basicAttackCooldownDecreaseAmount;

    public override void Use(PlayerStats playerStats)
    {
        _playerStats = playerStats;
        DeactivateVisuals();
        ActivateEffect();
        ActivateDuration();
    }

    protected override void ActivateEffect()
    {
        //Increase Mana
        _manaIncreaseAmount = _playerStats.MaxMana * _increasePercentage / 100;
        _playerStats.MaxMana += _manaIncreaseAmount;
        _playerStats.CurrentMana += _manaIncreaseAmount;

        //Increase Health
        _healthIncreaseAmount =  _playerStats.MaxHealth * (_increasePercentage / 100);
        _playerStats.MaxHealth += _healthIncreaseAmount;
        _playerStats.CurrentHealth += _healthIncreaseAmount;

        //Increase Move Speed
        _moveSpeedIncreaseAmount = _playerStats.MoveSpeed * (_increasePercentage / 100);
        _playerStats.MoveSpeed += _moveSpeedIncreaseAmount;
        
        //Increase Physical Power
        _physicalPowerIncreaseAmount = _playerStats.PhysicalPower * (_increasePercentage / 100);
        _playerStats.PhysicalPower += _physicalPowerIncreaseAmount;

        //Increase Ability Power
        _abilityPowerIncreaseAmount = _playerStats.AbilityPower * (_increasePercentage / 100);
        _playerStats.AbilityPower += _abilityPowerIncreaseAmount;

        //Decrease Basic Attack Cooldown
        _basicAttackCooldownDecreaseAmount = _playerStats.BasicAttackCooldown * (_increasePercentage / 100);
        _playerStats.BasicAttackCooldown = Mathf.Clamp(_playerStats.BasicAttackCooldown - _basicAttackCooldownDecreaseAmount, 0, float.MaxValue);

    }

    protected override void DeactivateEffect()
    {
        //MANA
        _playerStats.MaxMana -= _manaIncreaseAmount;
        _playerStats.CurrentMana -= _manaIncreaseAmount;

        //HEALTH
        _playerStats.MaxHealth -= _healthIncreaseAmount;
        _playerStats.CurrentHealth -= _healthIncreaseAmount;

        //MOVE SPEED
        _playerStats.MoveSpeed -= _moveSpeedIncreaseAmount;

        //PHYSICAL POWER
        _playerStats.PhysicalPower -= _physicalPowerIncreaseAmount;

        //ABILITY POWER
        _playerStats.AbilityPower -= _abilityPowerIncreaseAmount;

        //BASIC ATTACK COOLDOWN
        _playerStats.BasicAttackCooldown += _basicAttackCooldownDecreaseAmount;
        
    }
}
