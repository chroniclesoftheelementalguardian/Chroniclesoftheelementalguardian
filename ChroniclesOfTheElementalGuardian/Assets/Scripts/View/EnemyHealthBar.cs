
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private Image _healthBarImage;
    [SerializeField] private TextMeshProUGUI _healthText;
    private EnemyStats _enemyStats;

    private void Awake() 
    {
        _enemyStats = _enemy.GetEnemyStats();
    }

    private void Update()
    {
        _healthText.text = $"{Mathf.FloorToInt(_enemyStats.CurrentHealth)}";
        _healthBarImage.fillAmount = _enemyStats.CurrentHealth / _enemyStats.MaxHealth;
        _healthText.transform.forward = Camera.main.transform.forward;
    }
}
