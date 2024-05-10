using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsUI : MonoBehaviour
{
    [SerializeField] Transform panel;
    [SerializeField] Transform successPanel;
    

    private void Start() 
    {
        Enemy.Credits += OnCredits;
        Credits.CreditsFinished += OnCreditsFinished;
    }

    private void OnCredits()
    {
        panel.gameObject.SetActive(true);
    }

    private void OnCreditsFinished()
    {
        panel.gameObject.SetActive(false);
        successPanel.gameObject.SetActive(true);
    }

    private void OnDestroy() 
    {
        Enemy.Credits -= OnCredits;
        Credits.CreditsFinished -= OnCreditsFinished;
    }
}
