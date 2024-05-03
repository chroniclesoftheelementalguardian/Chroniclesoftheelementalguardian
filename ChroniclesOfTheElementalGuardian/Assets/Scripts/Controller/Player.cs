using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Player : MonoBehaviour, IDamagable
{
    [SerializeField] PlayerStats playerStats;
    
    PlayerMovement playerMovement;
    PlayerCombat playerCombat;

    Rigidbody2D rb2D;

    private void Awake() 
    {
        CacheComponents();
        SetupConstructors();
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        playerMovement.OnCollisionEnter2D(other);
    }

    private void CacheComponents()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    private void SetupConstructors()
    {
        playerMovement = new PlayerMovement(rb2D,transform,playerStats);
        playerCombat = new PlayerCombat(playerStats,transform);
    }

    public void TakeDamage(float damage, DamageType damageType)
    {
        float currentHealth = playerStats.CurrentHealth;
        currentHealth -= CalculateDamage(damage,damageType);
        currentHealth = Mathf.Clamp(currentHealth,0,playerStats.MaxHealth);
        if(currentHealth <= 0)
        {
            Die();
        }
        playerStats.CurrentHealth = currentHealth;
    }

    private float CalculateDamage(float damage, DamageType damageType)
    {
        switch(damageType)
        {
            case DamageType.Physical:
                damage -= playerStats.PhysicalArmor;
            break;

            case DamageType.Fire:
                damage -= playerStats.FireArmor;
            break;

            case DamageType.Water:
                damage -= playerStats.WaterArmor;
            break;

            case DamageType.Earth:
                damage -= playerStats.EarthArmor;
            break;

            case DamageType.Air:
                damage -= playerStats.AirArmor;
            break;

        }
        return damage;
    }

    private void Die()
    {

    }
}
