using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat
{
    PlayerStats playerStats;
    private Skill _selectedSkill;
    private List<Skill> _skills = new List<Skill>();
    private int _currentSelectedSkillID = 0;
    private Transform transform;
    
    private bool _isDefenseActive;
    private float _defenseCounter = float.MaxValue;

    public static event Action<Skill> SkillChanged;
    public static event Action NotEnoughMana;
    public static event Action Death;
    
    private void OnInputTab()
    {
        ChangeSelectedSkill();
    }

    private void OnInputR()
    {
        Defend();
    }

    private void OnInputE()
    {
        UseSkill();
    }

    private void OnInputSpace()
    {
        BasicAttack();
    }

    public PlayerCombat(PlayerStats playerStats, Transform transform)
    {
        this.playerStats = playerStats;
        this.transform = transform;
        RegisterEvents();
        ConstructSkills();
    }

    public void CountDownDefense()
    {
        if(!_isDefenseActive) return;
        if(_defenseCounter > playerStats.defenseDuration)
        {
            _isDefenseActive = false;
            _defenseCounter = float.MaxValue;
        }
        _defenseCounter += Time.deltaTime;
    }

    public void TakeDamage(float damage, DamageType damageType)
    {
        if(CanDefend(damageType)) return;
        float currentHealth = CalculateCurrentHealth(damage, damageType);
        if(IsDead(currentHealth)){ Death?.Invoke(); }
        playerStats.CurrentHealth = currentHealth;
    }

    private float CalculateCurrentHealth(float damage, DamageType damageType)
    {
        float currentHealth = playerStats.CurrentHealth;
        float finalDamage = CalculateDamage(damage, damageType);
        currentHealth -= finalDamage;
        currentHealth = Mathf.Clamp(currentHealth, 0, playerStats.MaxHealth);
        return currentHealth;
    }

    private bool IsDead(float currentHealth)
    {
        if (currentHealth <= 0) return true;
        return false;
    }

    private bool CanDefend(DamageType damageType)
    {
        if(_isDefenseActive && damageType == DamageType.Physical) 
        {
            Debug.Log("Defended An Attack");
            return true;
        }
        return false;
    }

    private void RegisterEvents()
    {
        InputReader.InputSpace += OnInputSpace;
        InputReader.InputE += OnInputE;
        InputReader.InputR += OnInputR;
        InputReader.InputTab += OnInputTab;
    }

    private void ConstructSkills()
    {
        _skills.Add(new FireSkill());
        _skills.Add(new EarthSkill());
        _skills.Add(new AirSkill());
        _skills.Add(new WaterSkill());
        _selectedSkill = _skills[_currentSelectedSkillID];
        SkillChanged?.Invoke(_selectedSkill);
    }

    private void BasicAttack()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.right, playerStats.meleeRange, playerStats.basicAttackLayerMask);
        
        if(hitInfo.collider != null)
        {
            IDamagable damagable;
            if(hitInfo.transform.TryGetComponent<IDamagable>(out damagable))
            {
                damagable.TakeDamage(playerStats.PhysicalPower, DamageType.Physical); 
            }
        }
    }

    private void Defend()
    {
        if(_isDefenseActive == true) return;
        Debug.Log("Defense Activated");
        _isDefenseActive = true;
        _defenseCounter = 0;
    }

    private void ChangeSelectedSkill()
    {
        _currentSelectedSkillID++;
        if(_currentSelectedSkillID >= _skills.Count){ _currentSelectedSkillID = 0; }
        _selectedSkill = _skills[_currentSelectedSkillID];
        SkillChanged?.Invoke(_selectedSkill);
    }

    private void UseSkill()
    {
        float currentMana = playerStats.CurrentMana;
        float manaCost = _selectedSkill.GetManaCost();
        if(currentMana < manaCost) 
        {
            Debug.Log("Not Enough Mana");
            NotEnoughMana?.Invoke();
            return;
        }
        _selectedSkill.Use(playerStats.AbilityPower, transform);
        currentMana = Mathf.Clamp(currentMana - manaCost, 0, playerStats.MaxMana);
        playerStats.CurrentMana = currentMana;
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
        damage = Mathf.Clamp(damage,0,damage);
        return damage;
    }
}
