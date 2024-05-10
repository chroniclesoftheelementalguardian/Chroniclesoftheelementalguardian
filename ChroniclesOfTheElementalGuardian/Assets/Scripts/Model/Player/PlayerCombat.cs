using System;
using System.Collections.Generic;
using ObjectPooling;
using UnityEngine;

public class PlayerCombat
{
    PlayerStats playerStats;
    private Skill _selectedSkill;
    private List<Skill> _skills = new List<Skill>();
    private int _currentSelectedSkillID = 0;
    private Transform transform;
    private GameObject shieldGameObject;
    
    private bool _isDefenseActive;
    private bool _isBasicAttackOnCooldown;
    private float _defenseCounter = float.MaxValue;
    private float _basicAttackCounter = float.MaxValue;

    public static event Action<Skill> SkillChanged;
    public static event Action NotEnoughMana;
    public static event Action Death;
    public static event Action CastingSpell;
    public static event Action Attacking;
    
    private void OnNextSkillPressed()
    {
        SelectNextSkill();
    }

    private void OnPreviousSkillPressed()
    {
        SelectPreviousSkill();
    }

    private void OnDefendPressed()
    {
        Defend();
    }

    private void OnSkillPressed()
    {
        UseSkill();
    }

    private void OnAttackPressed()
    {
        BasicAttack();
    }

    public PlayerCombat(PlayerStats playerStats, Transform transform, GameObject shieldGO)
    {
        this.playerStats = playerStats;
        this.transform = transform;
        shieldGameObject = shieldGO;
        RegisterEvents();
        ConstructSkills();
    }

    public void TakeDamage(float damage, DamageType damageType)
    {
        if(CanDefend(damageType)) return;
        float currentHealth = CalculateCurrentHealth(damage, damageType);
        if(IsDead(currentHealth)){ Die(); }
        playerStats.CurrentHealth = currentHealth;
    }

    private void Die()
    {
        Death?.Invoke();
        transform.gameObject.SetActive(false);
    }

    private float CalculateCurrentHealth(float damage, DamageType damageType)
    {
        float currentHealth = playerStats.CurrentHealth;
        float finalDamage = CalculateDamage(damage, damageType);
        SpawnDamageText(finalDamage);
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
            PoolObject poolObject = IObjectPool.GetFromPool("DamageText", true);
            poolObject.GetGameObject().GetComponent<DamageText>().SetupAsDefend();
            poolObject.SetWorldPosition(transform.position + Vector3.up * 3/4);
            return true;
        }
        return false;
    }

    private void RegisterEvents()
    {
        InputReader.AttackPressed += OnAttackPressed;
        InputReader.SkillPressed += OnSkillPressed;
        InputReader.DefendPressed += OnDefendPressed;
        InputReader.NextSkillPressed += OnNextSkillPressed;
        InputReader.PreviousSkillPressed += OnPreviousSkillPressed;
    }

    public void UnregisterEvents()
    {
        InputReader.AttackPressed -= OnAttackPressed;
        InputReader.SkillPressed -= OnSkillPressed;
        InputReader.DefendPressed -= OnDefendPressed;
        InputReader.NextSkillPressed -= OnNextSkillPressed;
        InputReader.PreviousSkillPressed -= OnPreviousSkillPressed;
    }

    private void ConstructSkills()
    {
        _skills.Add(new FireSkill());
        //_skills.Add(new EarthSkill());
        _skills.Add(new AirSkill());
        _skills.Add(new WaterSkill());
        _selectedSkill = _skills[_currentSelectedSkillID];
        SkillChanged?.Invoke(_selectedSkill);
    }

    private void BasicAttack()
    {
        if (_isBasicAttackOnCooldown) return;
        SpawnAttackFX();

        Attacking?.Invoke();
        _isBasicAttackOnCooldown = true;
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.right, playerStats.meleeRange, playerStats.basicAttackLayerMask);

        if (hitInfo.collider != null)
        {
            IDamagable damagable;
            if (hitInfo.transform.TryGetComponent<IDamagable>(out damagable))
            {
                damagable.TakeDamage(playerStats.PhysicalPower, DamageType.Physical);
            }
        }
    }

    private void SpawnAttackFX()
    {
        PoolObject poolObject = IObjectPool.GetFromPool("Swoosh", true);
        poolObject.SetWorldPosition(transform.position + transform.right / 2);
        poolObject.GetTransform().right = transform.right;
    }

    public void CountCooldowns()
    {
        CountDownBasicAttackCooldown();
        CountDownDefense();
    }

    public void CountDownBasicAttackCooldown()
    {
        if(!_isBasicAttackOnCooldown) return;
        if(_basicAttackCounter >= playerStats.BasicAttackCooldown)
        {
            _isBasicAttackOnCooldown = false;
            _basicAttackCounter = 0;
        }
        _basicAttackCounter += Time.deltaTime;
    }

    private void CountDownDefense()
    {
        if(!_isDefenseActive) return;
        if(_defenseCounter > playerStats.defenseDuration)
        {
            _isDefenseActive = false;
            _defenseCounter = float.MaxValue;
            shieldGameObject.SetActive(false);
        }
        _defenseCounter += Time.deltaTime;
    }

    
    private void Defend()
    {
        if(_isDefenseActive == true) return;
        shieldGameObject.SetActive(true);

        _isDefenseActive = true;
        _defenseCounter = 0;
    }

    private void SelectNextSkill()
    {
        _currentSelectedSkillID++;
        if(_currentSelectedSkillID >= _skills.Count){ _currentSelectedSkillID = 0; }
        _selectedSkill = _skills[_currentSelectedSkillID];
        SkillChanged?.Invoke(_selectedSkill);
    }

    private void SelectPreviousSkill()
    {
        _currentSelectedSkillID--;
        if(_currentSelectedSkillID < 0){ _currentSelectedSkillID = _skills.Count - 1; }
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
        CastingSpell?.Invoke();
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

    private void SpawnDamageText(float finalDamage)
    {
        PoolObject poolObject = IObjectPool.GetFromPool("DamageText", false);
        DamageText damageText = poolObject.GetGameObject().GetComponent<DamageText>();
        damageText.Setup(finalDamage);
        poolObject.SetWorldPosition(transform.position + Vector3.up * 3/4);
        poolObject.SetActive(true);
    }
}
