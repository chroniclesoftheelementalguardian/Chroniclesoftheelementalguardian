using UnityEngine;

public class RedPotion : Potion
{
    private float _cooldownDecreaseAmount;
    private float _physicalPowerIncreaseAmount;


    public override void Use(PlayerStats playerStats)
    {
        _playerStats = playerStats;
        DeactivateVisuals();
        ActivateEffect();
        ActivateDuration();
    }

    protected override void ActivateEffect()
    {
        _cooldownDecreaseAmount = _playerStats.BasicAttackCooldown * (_increasePercentage / 100);
        _physicalPowerIncreaseAmount = _playerStats.PhysicalPower * (_increasePercentage / 100);

        _playerStats.BasicAttackCooldown = Mathf.Clamp(_playerStats.BasicAttackCooldown - _cooldownDecreaseAmount, 0, float.MaxValue);
        _playerStats.PhysicalPower += _physicalPowerIncreaseAmount;
    }

    protected override void DeactivateEffect()
    {
        _playerStats.BasicAttackCooldown += _cooldownDecreaseAmount;
        _playerStats.PhysicalPower -= _physicalPowerIncreaseAmount;
    }
}
