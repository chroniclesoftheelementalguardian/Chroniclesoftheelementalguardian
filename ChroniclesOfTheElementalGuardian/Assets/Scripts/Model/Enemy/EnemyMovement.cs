using System;
using UnityEngine;
using DG.Tweening;

public class EnemyMovement
{
    EnemyStats _enemyStats;
    PlayerDetection _playerDetection;
    Player _player;
    Transform transform;
    private bool _canMoveToPlayer = false;
    
    public event Action<bool> Move;
    

    public EnemyMovement(EnemyStats enemyStats, PlayerDetection playerDetection, Transform transform, Player player)
    {
        _enemyStats = enemyStats;
        _playerDetection = playerDetection;
        _player = player;
        this.transform = transform;
        
        _playerDetection.playerEntered += OnPlayerEntered;
        _playerDetection.playerExit += OnPlayerExit;
    }

    private void OnPlayerExit()
    {
        _canMoveToPlayer = false;
    }

    private void OnPlayerEntered()
    {
        _canMoveToPlayer = true;
    }

    public void TryMoveToPlayer()
    {
        if (!_canMoveToPlayer) {Move?.Invoke(false); return;}
        MoveToPlayer();
    }

    private void MoveToPlayer()
    {
        Move?.Invoke(true);
        Vector3 direction = (_player.transform.position - transform.position).normalized;
        if(direction.x < 0)
        {
            transform.DORotate(new Vector3(0,180,0), 0f);
        }
        else
        {
            transform.DORotate(new Vector3(0,0,0), 0f);
        }
        transform.Translate(Vector3.right * Time.deltaTime * _enemyStats.MoveSpeed);
    }

    public bool CanAttack()
    {
        if(Vector2.Distance(_player.transform.position, transform.position) <= _enemyStats.MeleeRange)
        {
            Move?.Invoke(false);
            return true;
        }
        return false;
    }

    public void UnregisterEvents() 
    {
        _playerDetection.playerEntered -= OnPlayerEntered;
        _playerDetection.playerExit -= OnPlayerExit;
    }
}
