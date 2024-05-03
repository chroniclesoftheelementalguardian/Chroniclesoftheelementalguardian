using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerStats
{
    public float MoveSpeed; 
    public float JumpingPower; 
    public float LandingPower;
    public float MaxHealth;
    public float Damage;
    public float AbilityPower;
    public float PhysicalArmor;
    public float FireArmor;
    public float EarthArmor;
    public float WaterArmor;
    public float AirArmor;
    [HideInInspector] public float CurrentHealth;
}
