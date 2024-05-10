using System;
using ObjectPooling;
using UnityEngine;

public class EnemyCombat
{
    Player _player;
    EnemyStats _enemyStats;
    Transform transform;
    private bool _isRange;
    private float _meleeCooldownCounter = float.MaxValue;
    public float MeleeCooldownCounter {get {return _meleeCooldownCounter;}}

    public event Action Attacking;


    public EnemyCombat(EnemyStats enemyStats,Player player, Transform transform, bool isRange)
    {
        _enemyStats =enemyStats;
        _player = player;
        this.transform = transform;
        _isRange = isRange;
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
        if(!_isRange)
        {
            MeleeAttack();
        }
        else
        {
            ShootingAttack();
        }

        ResetCooldown();
    }

    private void MeleeAttack()
    {
        Attacking?.Invoke();
        SpawnAttackFX();
        
        _player.TakeDamage(_enemyStats.Damage, DamageType.Physical);
    }

    private void ShootingAttack()
    {
        PoolObject poolObject = IObjectPool.GetFromPool("Fireball",true);
        Projectile projectile = poolObject.GetGameObject().GetComponent<Projectile>();
        projectile.transform.position = transform.position;
        projectile.Shoot(
                            transform.right,
                            "Player",
                            10,
                            _enemyStats.Damage,
                            DamageType.Physical
                        );
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
