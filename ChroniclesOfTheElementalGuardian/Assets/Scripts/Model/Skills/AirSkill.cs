using System.Collections;
using System.Collections.Generic;
using ObjectPooling;
using UnityEngine;

public class AirSkill : Skill
{
    public override DamageType GetDamageType()
    {
        return DamageType.Air;
    }

    public override float GetManaCost()
    {
        return 10;
    }

    public override string GetSkillName()
    {
        return "Air";
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
        return "AirNebula";
    }
}
