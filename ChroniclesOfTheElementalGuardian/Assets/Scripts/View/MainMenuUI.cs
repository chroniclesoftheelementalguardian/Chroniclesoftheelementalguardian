using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] Button startButton, quitButton;

    private void Awake() 
    {
        startButton.onClick.AddListener(StartGame);
        quitButton.onClick.AddListener(QuitGame);    
    }

    private void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    private void QuitGame()
    {
        Application.Quit();
    }


    private void OnDisable() 
    {
        startButton.onClick.RemoveAllListeners();
        quitButton.onClick.RemoveAllListeners();  
    }
}
