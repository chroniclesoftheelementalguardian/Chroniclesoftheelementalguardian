using System.Collections.Generic;
using ObjectPooling;
using UnityEngine;

public class ElementalBlock : Block,IDamagable
{
    [SerializeField] private DamageType _requiredDamageType;
    [SerializeField] private float _damage;

    public void TakeDamage(float damage, DamageType damageType)
    {
        if(AreThereActiveEnemies()) return;
        if(damageType != _requiredDamageType)
        {
            ShootBack(damageType);
            return;
        }
        //Spawn VFX
        
        gameObject.SetActive(false);

    }

    private bool AreThereActiveEnemies()
    {
        if(activeEnemies.Count > 0) return true;
        return false;
    }

    private void ShootBack(DamageType damageType)
    {
        switch(damageType)
        {
            case DamageType.Fire:
            SpawnProjectile("Fireball", DamageType.Fire);
            break;

            case DamageType.Air:
            SpawnProjectile("AirNebula", DamageType.Air);
            break;

            case DamageType.Earth:
            SpawnProjectile("EarthProjectile", DamageType.Earth);
            break;

            case DamageType.Water:
            SpawnProjectile("WaterArrow", DamageType.Water);
            break;
        }
    }

    private void SpawnProjectile(string projectileTag, DamageType damageType)
    {
        PoolObject poolObject = IObjectPool.GetFromPool(projectileTag,true);
        poolObject.SetWorldPosition(transform.position);
        Projectile projectile = poolObject.GetGameObject().GetComponent<Projectile>();
        projectile.Shoot(Vector2.left,"Player",5,_damage,damageType);
    }

    protected override void TryDisableBlock()
    {
        // Do nothing.
    }
}