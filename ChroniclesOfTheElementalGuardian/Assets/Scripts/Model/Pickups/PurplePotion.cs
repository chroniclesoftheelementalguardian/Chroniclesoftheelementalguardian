public class PurplePotion : Potion
{
    public override void Use(PlayerStats playerStats)
    {
        _playerStats = playerStats;
        DeactivateVisuals();
        ActivateEffect();
        SpawnEffectText("Double Jump");
    }

    protected override void ActivateEffect()
    {
        _playerStats.CanDoubleJump = true;
    }

    protected override void DeactivateEffect()
    {
        _playerStats.CanDoubleJump = false;
    }
}
