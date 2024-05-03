using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStats", menuName = "EnemyStats", order = 0)]
public class EnemyStats : ScriptableObject 
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpingPower;
    [SerializeField] private float _landingPower;
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _damage;
    [SerializeField] private float _abilityPower;
    [SerializeField] private float _physicalArmor;
    [SerializeField] private float _fireArmor;
    [SerializeField] private float _earthArmor;
    [SerializeField] private float _waterArmor;
    [SerializeField] private float _airArmor;

     public float MoveSpeed {get {return _moveSpeed;}}
     public float JumpingPower {get {return _jumpingPower;}}
     public float LandingPower {get {return _landingPower;}}
     public float MaxHealth {get {return _maxHealth;}}
     public float Damage {get {return _damage;}}
     public float AbilityPower {get {return _abilityPower;}}
     public float PhysicalArmor {get {return _physicalArmor;}}
     public float FireArmor {get {return _fireArmor;}}
     public float EarthArmor {get {return _earthArmor;}}
     public float WaterArmor {get {return _waterArmor;}}
     public float AirArmor {get {return _airArmor;}}
}
