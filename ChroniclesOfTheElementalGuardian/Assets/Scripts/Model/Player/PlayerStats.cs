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
    
    
    public float MaxMana;
    public float ManaRegPercentage;
    

    public float RegCooldown;
    public float BasicAttackCooldown;
    
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

    [HideInInspector] public bool CanDoubleJump = false;
    [HideInInspector] public float CurrentHealth;
    [HideInInspector] public float CurrentMana;
    
}
