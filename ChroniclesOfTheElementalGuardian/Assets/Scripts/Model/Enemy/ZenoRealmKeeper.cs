using System.Collections.Generic;
using UnityEngine;

public class ZenoRealmKeeper : MonoBehaviour
{
    [SerializeField] List<Enemy> activeEnemies;
    [SerializeField] Enemy enemy;
    private EnemyMovement enemyMovement;
    Rigidbody2D rb2D;
    private bool _isMoving;
    
    private void Awake() 
    {
        RegisterEvents();
        rb2D = GetComponent<Rigidbody2D>();
    }

    private void Start() 
    {
        enemyMovement = GetComponent<Enemy>().GetEnemyMovement();
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
            TryActivate();
        }
    }

    private void OnDisable() 
    {
        Enemy.Death -= RemoveEnemy;
    }

    private void TryActivate()
    {
        if(activeEnemies.Count > 0) return;
        _isMoving = true;
    }

    private void Update()
    {
        if(!_isMoving) return;
        enemyMovement.MoveToPlayer();
    }

    public void Jump()
    {
        _isMoving = false;
        rb2D.AddForce(Vector2.left * 4 + Vector2.up * 3, ForceMode2D.Impulse);
    }
}
