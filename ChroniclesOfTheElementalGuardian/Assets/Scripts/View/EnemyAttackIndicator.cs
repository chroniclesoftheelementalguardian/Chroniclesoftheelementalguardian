using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAttackIndicator : MonoBehaviour
{
    [SerializeField] private Enemy enemy;
    [SerializeField] private Image _barImage;
    private EnemyCombat enemyCombat;

    private void Start() {
        enemyCombat = enemy.GetEnemyCombat();
    }

    private void Update() 
    {
        _barImage.fillAmount = Mathf.Clamp(enemyCombat.MeleeCooldownCounter / enemyCombat.GetAttackCooldown(), 0,1);
    }
}
