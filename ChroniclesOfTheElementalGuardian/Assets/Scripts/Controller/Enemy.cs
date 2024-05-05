using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable
{
    [SerializeField] private EnemyStats enemyStats;
    [SerializeField] private PlayerDetection playerDetection;
    [SerializeField] private Animator animator;
    Player _player;
    EnemyMovement enemyMovement;
    EnemyCombat enemyCombat;
    EnemyAnimator enemyAnimator;

    private float _currentHealth;

    private void Awake() 
    {
        _player = FindObjectOfType<Player>();
        _currentHealth = enemyStats.MaxHealth;
        enemyMovement = new EnemyMovement(enemyStats,playerDetection,transform,_player);
        enemyCombat = new EnemyCombat(enemyStats,_player);
        enemyAnimator = new EnemyAnimator(animator,enemyCombat,enemyMovement);
    }

    private void Update() 
    {
        if(enemyMovement.CanAttack())
        {
            enemyCombat.Attack();
            return;
        }
        enemyMovement.TryMoveToPlayer();
    }

    public void TakeDamage(float damage, DamageType damageType)
    {
        Debug.Log($"Enemy: Incoming Damage: {damage}, DamageType: {damageType}");
        float finalDamage = CalculateDamage(damage,damageType);
        _currentHealth -= finalDamage;
        _currentHealth = Mathf.Clamp(_currentHealth,0,enemyStats.MaxHealth);
        Debug.Log($"Enemy Took Damage Amount: {finalDamage}, CurrentHealth: {_currentHealth}");
        if(_currentHealth <= 0)
        {
            Die();
        }
    }

    private float CalculateDamage(float damage, DamageType damageType)
    {
        switch(damageType)
        {
            case DamageType.Physical:
                damage -= enemyStats.PhysicalArmor;
            break;

            case DamageType.Fire:
                damage -= enemyStats.FireArmor;
            break;

            case DamageType.Water:
                damage -= enemyStats.WaterArmor;
            break;

            case DamageType.Earth:
                damage -= enemyStats.EarthArmor;
            break;

            case DamageType.Air:
                damage -= enemyStats.AirArmor;
            break;

        }
        damage = Mathf.Clamp(damage,0,damage);
        return damage;
    }

    private void Die()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable() 
    {
        enemyMovement.UnregisterEvents();
    }
}
