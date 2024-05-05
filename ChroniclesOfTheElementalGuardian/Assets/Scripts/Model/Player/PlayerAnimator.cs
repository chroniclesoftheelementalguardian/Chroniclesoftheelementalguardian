using System;
using UnityEngine;

public class PlayerAnimator
{
    private Animator _animator;

    private bool _moveRight;
    private bool _moveLeft;

    public PlayerAnimator (Animator animator)
    {
        _animator = animator;
        RegisterEvents();
    }

    private void Run(bool isRunning)
    {
        _animator.SetBool("IsRunning", isRunning);
    }

    private void Attack()
    {
        Debug.Log("Attack");
        _animator.SetTrigger("Attack");
    }

    private void CastSpell()
    {
        Debug.Log("CastSpell");
        _animator.SetTrigger("Cast");
    }

    private void OnAttackPressed()
    {
        Attack();
    }

    private void OnCastingSpell()
    {
        CastSpell();
    }

    private void OnMoveRightPressed()
    {
        _moveRight = true;
        Run(true);
    }

    private void OnMoveLeftPressed()
    {
        _moveLeft = true;
        Run(true);
    }

    private void OnMoveRightUnpressed()
    {
        _moveRight = false;
        if(!_moveLeft && !_moveRight)
        {
            Run(false);
        }
    }

    private void OnMoveLeftUnpressed()
    {
        _moveLeft = false;
        if(!_moveLeft && !_moveRight)
        {
            Run(false);
        }
    }

    private void RegisterEvents()
    {
        InputReader.MoveLeftPressed += OnMoveLeftPressed;
        InputReader.MoveRightPressed += OnMoveRightPressed;
        InputReader.MoveLeftUnpressed += OnMoveLeftUnpressed;
        InputReader.MoveRightUnpressed += OnMoveRightUnpressed;
        PlayerCombat.Attacking += OnAttackPressed;
        PlayerCombat.CastingSpell += OnCastingSpell;
    }

    public void UnregisterEvents()
    {
        InputReader.MoveLeftPressed -= OnMoveLeftPressed;
        InputReader.MoveRightPressed -= OnMoveRightPressed;
        InputReader.MoveLeftUnpressed -= OnMoveLeftUnpressed;
        InputReader.MoveRightUnpressed -= OnMoveRightUnpressed;
        PlayerCombat.Attacking -= OnAttackPressed;
        PlayerCombat.CastingSpell -= OnCastingSpell;
    }

    
}
