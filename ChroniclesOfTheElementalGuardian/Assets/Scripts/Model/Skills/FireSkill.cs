using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSkill : Skill
{
    public override DamageType GetDamageType()
    {
        return DamageType.Fire;
    }

    public override float GetManaCost()
    {
        return 10;
    }

    public override string GetSkillName()
    {
        return "Fireball";
    }

    public override void Use(float abilityPower, Transform userTransform)
    {
        SpawnProjectile(
            userTransform.position,
            userTransform.right,
            "Enemy",
            10,
            abilityPower
        );
    }

    protected override string GetProjectileTag()
    {
        return "Fireball";
    }
}
