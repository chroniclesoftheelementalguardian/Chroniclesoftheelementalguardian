using System.Collections;
using System.Collections.Generic;
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
        playerCombat = new PlayerCombat(playerStats);
    }

    public void TakeDamage(float damage)
    {
        float currentHealth = playerStats.CurrentHealth;
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth,0,playerStats.MaxHealth);
        if(currentHealth <= 0)
        {
            Die();
        }
        playerStats.CurrentHealth = currentHealth;
    }

    private void Die()
    {

    }
}
