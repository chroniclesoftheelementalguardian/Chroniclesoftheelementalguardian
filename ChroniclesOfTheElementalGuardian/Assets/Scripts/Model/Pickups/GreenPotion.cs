public class GreenPotion : Potion
{
    PlayerStats _playerStats;
    private float _increaseAmount;

    public override void Use(PlayerStats playerStats)
    {
        _playerStats = playerStats;
        ActivateEffect();
        DeactivateVisuals();
    }

    private void ActivateEffect()
    {
        _increaseAmount =  _playerStats.MaxHealth * (_increasePercentage / 100);

        _playerStats.MaxHealth += _increaseAmount;
        _playerStats.CurrentHealth += _increaseAmount;
    }

    protected override void DeactivateEffect()
    {
        _playerStats.MaxHealth -= _increaseAmount;
        _playerStats.CurrentHealth -= _increaseAmount;
    }
}
