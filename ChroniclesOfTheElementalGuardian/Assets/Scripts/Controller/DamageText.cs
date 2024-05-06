using System.Collections;
using System.Collections.Generic;
using ObjectPooling;
using TMPro;
using UnityEngine;

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
            damageText.color = Color.green;
        }
        else
        {
            damageText.text = $"{damage:F1} Damage";
            damageText.color = Color.red;
        }
    }
}
