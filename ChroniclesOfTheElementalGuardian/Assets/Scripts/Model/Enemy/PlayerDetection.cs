using System;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    public event Action playerEntered;
    public event Action playerExit;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Player"))
        {
            playerEntered?.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if(other.CompareTag("Player"))
        {
            playerExit?.Invoke();
        }
    }
}
