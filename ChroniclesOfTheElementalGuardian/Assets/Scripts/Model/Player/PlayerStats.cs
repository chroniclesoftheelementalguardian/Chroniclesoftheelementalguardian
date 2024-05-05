using System;
using UnityEngine;

[Serializable]
public class PlayerStats
{
    public float MoveSpeed; 
    public float JumpingPower; 
    public float LandingPower;
    
    public float MaxHealth;
    public float HealthRegPercentage;
    [HideInInspector]public float CurrentHealth;
    
    public float MaxMana;
    public float ManaRegPercentage;
    [HideInInspector]public float CurrentMana;

    public float RegCooldown;
    
    public float PhysicalPower;
    public float AbilityPower;
    public float PhysicalArmor;
    public float FireArmor;
    public float EarthArmor;
    public float WaterArmor;
    public float AirArmor;
    public float meleeRange;
    public float defenseDuration = 0.5f;
    public LayerMask basicAttackLayerMask;
    
}
