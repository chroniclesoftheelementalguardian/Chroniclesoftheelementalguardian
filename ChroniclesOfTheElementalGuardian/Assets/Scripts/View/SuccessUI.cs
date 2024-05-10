using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SuccessUI : MonoBehaviour
{
    [SerializeField] Button nextLevelButton,restartButton,quitButton;
    [SerializeField] Transform panel;

    private void Awake() 
    {
        ZenoRealmKeeper.PlayerSuccess += OnSuccess;
    }

    private void OnSuccess()
    {
        panel.gameObject.SetActive(true);
    }

    private void OnEnable() 
    {
        nextLevelButton.onClick.AddListener(NextLevel);
        restartButton.onClick.AddListener(Restart);
        quitButton.onClick.AddListener(Quit);
    }

    private void Quit()
    {
        Application.Quit();
    }

    private void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void NextLevel()
    {
        int currentBuildIndex = SceneManager.GetActiveScene().buildIndex;
        currentBuildIndex++;
        Debug.Log("Current Build Index: " + currentBuildIndex);
        if(currentBuildIndex > 2)
        {
            currentBuildIndex = 1;
        }
        SceneManager.LoadScene(currentBuildIndex);
    }

    private void OnDisable() 
    {
        nextLevelButton.onClick.RemoveAllListeners();
        restartButton.onClick.RemoveAllListeners();
        quitButton.onClick.RemoveAllListeners();
    }

    private void OnDestroy() 
    {
        ZenoRealmKeeper.PlayerSuccess -= OnSuccess;
    }

}
