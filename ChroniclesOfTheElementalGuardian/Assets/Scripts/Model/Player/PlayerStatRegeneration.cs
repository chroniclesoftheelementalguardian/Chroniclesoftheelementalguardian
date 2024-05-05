using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatRegeneration
{
    private PlayerStats _playerStats;
    private float _cooldownCounter = float.MaxValue;
    private bool _isCooldownActive;

    public PlayerStatRegeneration(PlayerStats playerStats)
    {
        _playerStats = playerStats;
    }

    public void RegenerateStats()
    {
        if(!_isCooldownActive){ 
                RegenerateHealth();
                RegenerateMana();
                ActivateCooldown();
                return; 
            }
        Cooldown();
        
    }

    private void RegenerateMana()
    {
        float currentMana = _playerStats.CurrentMana;
        currentMana += _playerStats.MaxMana * (_playerStats.ManaRegPercentage / 100);
        currentMana = Mathf.Clamp(currentMana, 0, _playerStats.MaxMana);
        _playerStats.CurrentMana = currentMana;
    }

    private void RegenerateHealth()
    {
        float currentHealth = _playerStats.CurrentHealth;
        currentHealth += _playerStats.MaxHealth * (_playerStats.HealthRegPercentage / 100);
        currentHealth = Mathf.Clamp(currentHealth,0,_playerStats.MaxHealth);
        _playerStats.CurrentHealth = currentHealth;
    }

    private void Cooldown()
    {
        if(_cooldownCounter >= _playerStats.RegCooldown)
        {
            _isCooldownActive = false;
            return;
        }
        _cooldownCounter += Time.deltaTime;
    }

    private void ActivateCooldown()
    {
        _isCooldownActive = true;
        _cooldownCounter = 0;
    }
}
