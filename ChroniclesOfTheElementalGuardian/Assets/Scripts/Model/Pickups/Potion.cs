using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Potion : MonoBehaviour
{
    [SerializeField] protected GameObject _model;
    [SerializeField] protected float _increasePercentage;
    [SerializeField] protected float _effectDuration;
    protected Collider2D _collider;

    private float _durationCounter;
    private bool _isDurationActive;

    public abstract void Use(PlayerStats playerStats);
    protected abstract void DeactivateEffect();

    private void Awake() 
    {
        _collider = GetComponent<Collider2D>();
    }

    protected void ActivateDuration()
    {
        _isDurationActive = true;
    }

    protected void DeactivateVisuals()
    {
        _model.SetActive(false);
        _collider.enabled = false;
    }
    
    protected void Update()
    {
        if(!_isDurationActive) return;
        CountDownDuration();
    }

    protected void CountDownDuration()
    {
        if(_durationCounter >= _effectDuration)
        {
            _isDurationActive = false;
            DeactivateEffect();
        }
        _durationCounter += Time.deltaTime;
    }   
}
