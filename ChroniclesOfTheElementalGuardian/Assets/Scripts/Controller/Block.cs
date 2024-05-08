using System;
using System.Collections.Generic;
using ObjectPooling;
using UnityEngine;

public class Block : MonoBehaviour 
{
    [SerializeField] protected List<Enemy> activeEnemies;
    [SerializeField] private GameObject blockedGameObject;
    [SerializeField] private Transform _fxSpawnTransform;

    private void Awake() 
    {
        RegisterEvents();
        if(blockedGameObject == null) return;
        blockedGameObject.SetActive(false);
    }

    private void RegisterEvents()
    {
        Enemy.Death += RemoveEnemy;
    }

    private void RemoveEnemy(Enemy enemy)
    {
        if(activeEnemies.Contains(enemy))
        {
            activeEnemies.Remove(enemy);
            TryDisableBlock();
        }
    }

    protected virtual void TryDisableBlock()
    {
        if(activeEnemies.Count > 0) return;
        //Spawn VFX.
        if(blockedGameObject != null){ blockedGameObject.SetActive(true); }
        PoolObject poolObject = IObjectPool.GetFromPool("BlockDisableFX", true);
        poolObject.SetWorldPosition(_fxSpawnTransform.position);
        gameObject.SetActive(false);
    }

    private void OnDestroy() 
    {
        Enemy.Death -= RemoveEnemy;
    }
}