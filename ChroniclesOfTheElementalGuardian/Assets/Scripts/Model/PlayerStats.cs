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
    [HideInInspector] public float CurrentHealth;
}
