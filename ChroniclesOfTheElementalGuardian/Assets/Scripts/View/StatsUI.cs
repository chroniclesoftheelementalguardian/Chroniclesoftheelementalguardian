using TMPro;
using UnityEngine;

public class StatsUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _physicalPowerText;
    [SerializeField] TextMeshProUGUI _abilityPowerText;
    [SerializeField] TextMeshProUGUI _currentHealthText;
    [SerializeField] TextMeshProUGUI _currentManaText;

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
        _physicalPowerText.text = $"Physical Power: {playerStats.PhysicalPower}";
        _abilityPowerText.text = $"Ability Power: {playerStats.AbilityPower}";
        _currentHealthText.text = $"Health: {Mathf.FloorToInt(playerStats.CurrentHealth)}";
        _currentManaText.text = $"Mana: {Mathf.FloorToInt(playerStats.CurrentMana)}";
    }
}
