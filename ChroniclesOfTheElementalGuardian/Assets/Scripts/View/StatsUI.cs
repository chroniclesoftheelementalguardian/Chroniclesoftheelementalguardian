using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatsUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _physicalPowerText;
    [SerializeField] TextMeshProUGUI _abilityPowerText;
    [SerializeField] TextMeshProUGUI _currentHealthText;
    [SerializeField] TextMeshProUGUI _currentManaText;
    [SerializeField] Image _healthBarImage,_manaBarImage;

    [SerializeField] private Player player;
    private PlayerStats playerStats;



    private void Awake() 
    {
        playerStats = player.GetPlayerStats();
    }

    private void Update() 
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        _physicalPowerText.text = $"{playerStats.PhysicalPower}";
        _abilityPowerText.text = $"{playerStats.AbilityPower}";
        _currentHealthText.text = $"{Mathf.FloorToInt(playerStats.CurrentHealth)} / {Mathf.FloorToInt(playerStats.MaxHealth)}";
        _currentManaText.text = $"{Mathf.FloorToInt(playerStats.CurrentMana)} / {Mathf.FloorToInt(playerStats.MaxMana)}";
        _healthBarImage.fillAmount = playerStats.CurrentHealth / playerStats.MaxHealth;
        _manaBarImage.fillAmount = playerStats.CurrentMana / playerStats.MaxMana;
    }
}
