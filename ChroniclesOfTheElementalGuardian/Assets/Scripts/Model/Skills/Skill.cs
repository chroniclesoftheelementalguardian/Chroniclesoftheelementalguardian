using UnityEngine;
using ObjectPooling;

public abstract class Skill
{
    public abstract void Use(float abilityPower, Transform userTransform);
    public abstract DamageType GetDamageType();
    public abstract string GetSkillName();
    protected abstract string GetProjectileTag();
    public abstract float GetManaCost();

    protected void SpawnProjectile(Vector3 spawnPos, Vector3 direction,string targetTag, float speed,float abilityPower)
    {
        PoolObject poolObject = IObjectPool.GetFromPool(GetProjectileTag(),true);
        Projectile projectile = poolObject.GetGameObject().GetComponent<Projectile>();
        projectile.transform.position = spawnPos;
        projectile.Shoot(
                            direction,
                            targetTag,
                            speed,
                            abilityPower,
                            GetDamageType()
                        );
    }
}
