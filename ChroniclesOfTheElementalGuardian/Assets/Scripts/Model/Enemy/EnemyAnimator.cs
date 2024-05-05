using UnityEngine;

public class EnemyAnimator
{
    private Animator _animator;
    private EnemyCombat _enemyCombat;
    private EnemyMovement _enemyMovement;

    public EnemyAnimator(Animator animator,EnemyCombat enemyCombat,EnemyMovement enemyMovement)
    {
        _animator = animator;
        _enemyCombat = enemyCombat;
        _enemyMovement = enemyMovement;

        RegisterEvents();
    }

    private void OnCast()
    {
        Cast();
    }

    private void OnAttack()
    {
        Attack();
    }

    private void OnMove(bool isMoving)
    {
        Move(isMoving);
    }

    private void Move(bool isMoving)
    {
        _animator.SetBool("IsRunning", isMoving);
    }

    private void Attack()
    {
        _animator.SetTrigger("Attack");
    }

    private void Cast()
    {
        _animator.SetTrigger("Cast");
    }

    private void RegisterEvents()
    {
        _enemyCombat.Attacking += OnAttack;
        _enemyMovement.Move += OnMove;
    }

    public void UnregisterEvents()
    {
        _enemyCombat.Attacking -= OnAttack;
        _enemyMovement.Move -= OnMove;
    }

}
