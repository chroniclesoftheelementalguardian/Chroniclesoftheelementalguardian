using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;

[Serializable]
public class PlayerMovement
{
    PlayerStats playerStats;
    private Rigidbody2D _rb2D;
    private Transform transform;
    private bool _isOnAir;
    private bool _isLanding;

    public PlayerMovement(Rigidbody2D rb2D, Transform transform,PlayerStats playerStats)
    {
        _rb2D = rb2D;
        this.transform = transform;
        this.playerStats = playerStats;
        RegisterEvents();
    }

    private void OnInputA()
    {
        transform.DORotate(new Vector3(0,180,0), 0f);
        Move(Vector2.right);
        
    }

    private void OnInputD()
    {
        transform.DORotate(new Vector3(0,0,0), 0f);
        Move(Vector2.right);
    }

    private void OnInputW()
    {
        if(_isOnAir) return;
        Jump();
    }

    private void OnInputS()
    {
        if(!_isOnAir) return;
        if(_isLanding) return;
        Land();
    }

    private void Move(Vector2 direction)
    {
        transform.Translate(direction * Time.deltaTime * playerStats.MoveSpeed);
    }

    private void Jump()
    {
        _rb2D.AddForce(Vector2.up * playerStats.JumpingPower, ForceMode2D.Impulse);
        _isOnAir = true;
    }

    private void Land()
    {
        _isLanding = true;
        _rb2D.AddForce(Vector2.down * playerStats.LandingPower, ForceMode2D.Impulse);
    }

    public void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.CompareTag("Ground"))
        {
            _isLanding = false;
            _isOnAir = false;
        }
    }

    public void RegisterEvents()
    {
        InputReader.InputA += OnInputA;
        InputReader.InputD += OnInputD;
        InputReader.InputW += OnInputW;
        InputReader.InputS += OnInputS;
    }

    public void UnregisterEvents()
    {

    }
}
