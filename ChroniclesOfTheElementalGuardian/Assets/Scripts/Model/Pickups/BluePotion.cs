public class BluePotion : Potion
{
    public override void Use(PlayerStats playerStats)
    {
        _playerStats = playerStats;
        ActivateEffect();
        SpawnEffectText("Mana++");
        DeactivateVisuals();
    }

    protected override void ActivateEffect()
    {
        _increaseAmount =  _playerStats.MaxMana * (_increasePercentage / 100);

        _playerStats.MaxMana += _increaseAmount;
        _playerStats.CurrentMana += _increaseAmount;
    }

    protected override void DeactivateEffect()
    {
        _playerStats.MaxMana -= _increaseAmount;
        _playerStats.CurrentMana -= _increaseAmount;
    }
}
