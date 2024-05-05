using UnityEngine;

public class EnemyCombat
{
    Player _player;
    EnemyStats _enemyStats;
    private float _meleeCooldownCounter = float.MaxValue;

    public EnemyCombat(EnemyStats enemyStats,Player player)
    {
        _enemyStats =enemyStats;
        _player = player;
    }

    public void Attack()
    {
        if(Cooldown()) return;
        _player.TakeDamage(_enemyStats.Damage, DamageType.Physical);
        ResetCooldown();
    }

    private bool Cooldown()
    {
        if(_meleeCooldownCounter < _enemyStats.MeleeCooldown)
        {
            _meleeCooldownCounter += Time.deltaTime;
            return true;
        }
        return false;
    }

    private void ResetCooldown()
    {
        _meleeCooldownCounter = 0;
    }
}
