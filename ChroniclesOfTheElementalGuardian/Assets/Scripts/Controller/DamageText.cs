using System.Collections;
using System.Collections.Generic;
using ObjectPooling;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DamageText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI damageText;

    void OnFinished()
    {
        IObjectPool.Release(transform);
    }

    public void Setup(float damage)
    {
        if(damage <= 0)
        {
            damageText.text = "Resisted";
            damageText.color = Color.blue;
        }
        else
        {
            damageText.text = $"{damage:F1} Damage";
            damageText.color = Color.red;
        }
    }

    public void SetupAsDefend()
    {
        damageText.text = "Defend";
        damageText.color = Color.blue;
    }

    public void SetupAsPotion(string effectText)
    {
        damageText.text = effectText;
        if(SceneManager.GetActiveScene().buildIndex == 2)
        {
            damageText.color = Color.green;
        }
        else
        {
            damageText.color = Color.blue;
        }
        
    }
}
