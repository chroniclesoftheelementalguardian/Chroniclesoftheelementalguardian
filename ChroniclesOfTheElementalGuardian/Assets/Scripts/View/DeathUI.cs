using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeathUI : MonoBehaviour 
{
    [SerializeField] private Button restartButton,quitButton;
    [SerializeField] private Transform _panel;

    private void Awake() 
    {
        PlayerCombat.Death += OnGameFinished;
        DeathTrigger.GameFinished += OnGameFinished;
    }

    private void OnEnable() 
    {
        restartButton.onClick.AddListener(RestartLevel);
        quitButton.onClick.AddListener(QuitGame);
    }

    private void OnGameFinished()
    {
        _panel.gameObject.SetActive(true);
    }

    private void RestartLevel()
    {
        _panel.gameObject.SetActive(true);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }   

    private void QuitGame()
    {
        Application.Quit();
    }

    private void OnDisable() 
    {
        restartButton.onClick.RemoveAllListeners();
        quitButton.onClick.RemoveAllListeners();
    }

    private void OnDestroy() 
    {
        DeathTrigger.GameFinished -= OnGameFinished;
        PlayerCombat.Death -= OnGameFinished;
    }
}