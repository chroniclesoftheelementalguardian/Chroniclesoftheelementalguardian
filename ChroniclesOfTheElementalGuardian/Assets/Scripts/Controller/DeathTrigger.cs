using System;
using Cinemachine;
using UnityEngine;

public class DeathTrigger : MonoBehaviour
{
    public static event Action GameFinished;
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Player"))
        {
            GameFinished?.Invoke();
        }
    }
}
