using System;
using ObjectPooling;
using UnityEngine;

public class EnemyCombat
{
    Player _player;
    EnemyStats _enemyStats;
    Transform transform;
    private float _meleeCooldownCounter = float.MaxValue;
    public float MeleeCooldownCounter {get {return _meleeCooldownCounter;}}

    public event Action Attacking;


    public EnemyCombat(EnemyStats enemyStats,Player player, Transform transform)
    {
        _enemyStats =enemyStats;
        _player = player;
        this.transform = transform;
    }

    public void AttackCooldown()
    {
        if(_meleeCooldownCounter < _enemyStats.MeleeCooldown)
        {
            _meleeCooldownCounter += Time.deltaTime;
        }
    }

    public void Attack()
    {
        if(Cooldown()) return;
        Attacking?.Invoke();
        SpawnAttackFX();
        
        _player.TakeDamage(_enemyStats.Damage, DamageType.Physical);
        ResetCooldown();
    }

    private void SpawnAttackFX()
    {
        PoolObject poolObject = IObjectPool.GetFromPool("Swoosh", true);
        poolObject.SetWorldPosition(transform.position + transform.right / 2);
        poolObject.GetTransform().right = transform.right;
    }

    private bool Cooldown()
    {
        if(_meleeCooldownCounter < _enemyStats.MeleeCooldown)
        {
            return true;
        }
        return false;
    }

    private void ResetCooldown()
    {
        _meleeCooldownCounter = 0;
    }

    public float GetAttackCooldown()
    {
        return _enemyStats.MeleeCooldown;
    }
}
