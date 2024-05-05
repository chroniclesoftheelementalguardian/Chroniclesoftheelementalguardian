using System;
using UnityEngine;

public class EnemyMovement
{
    EnemyStats _enemyStats;
    PlayerDetection _playerDetection;
    Player _player;
    Transform transform;
    private bool _canMoveToPlayer = false;
    

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
        if (!_canMoveToPlayer) return;
        MoveToPlayer();
    }

    private void MoveToPlayer()
    {
        Vector3 direction = (_player.transform.position - transform.position).normalized;
        transform.Translate(Vector3.right * direction.x * Time.deltaTime * _enemyStats.MoveSpeed);
    }

    public bool CanAttack()
    {
        if(Vector2.Distance(_player.transform.position, transform.position) <= _enemyStats.MeleeRange)
        {
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
