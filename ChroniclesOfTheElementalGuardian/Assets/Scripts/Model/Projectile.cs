using ObjectPooling;
using UnityEngine;
using DG.Tweening;

public class Projectile : MonoBehaviour
{
    private bool _isActive = false;
    private float _damage;
    private float _speed;
    private string _targetTag;
    private Vector3 _direction;
    private DamageType _damageType;

    public virtual void Shoot(Vector2 direction,string targetTag ,float speed, float damage, DamageType damageType)
    {
        _targetTag = targetTag;
        _speed = speed;
        _damage = damage;
        _damageType = damageType;
        _direction = direction;
        Rotate(_direction);
    
        _isActive = true;
    }

    void Update()
    {
        if(!_isActive) return;
        Travel();
    }

    private void Travel()
    {
        transform.Translate(transform.right * _direction.x * Time.deltaTime * _speed);
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag(_targetTag))
        {
            other.TryGetComponent<IDamagable>(out IDamagable target);
            target.TakeDamage(_damage,_damageType);
            Reset();
        }
    }

    private void Reset()
    {
        _isActive = false;
        _damage = 0;
        _speed = 0;
        _targetTag = null;
        _damageType = DamageType.Physical;
        IObjectPool.Release(transform);
    }

    private void Rotate(Vector3 direction)
    {
        if(direction.x < 0)
        {
            transform.DORotate(new Vector3(0,180,0), 0);
        }
        if(direction.x > 0)
        {
            transform.DORotate(new Vector3(0,0,0), 0);
        }
    }


}
