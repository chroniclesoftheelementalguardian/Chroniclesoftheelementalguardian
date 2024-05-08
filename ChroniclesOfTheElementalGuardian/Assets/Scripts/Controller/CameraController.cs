using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private CinemachineVirtualCamera cinemachineVirtualCamera;
    
    private void Awake() 
    {
        cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
        DeathTrigger.GameFinished += OnGameFinished;
    }

    private void OnGameFinished()
    {
        cinemachineVirtualCamera.m_Follow = null;
    }

    private void OnDestroy() 
    {
        DeathTrigger.GameFinished -= OnGameFinished;
    }
}
