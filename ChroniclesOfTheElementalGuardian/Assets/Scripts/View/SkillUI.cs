using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SkillUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _skillText;
    private void Awake() 
    {
        PlayerCombat.SkillChanged += OnSkillChanged;
    }

    private void OnSkillChanged(Skill selectedSkill)
    {
        UpdateUI(selectedSkill);
    }

    private void UpdateUI(Skill selectedSkill)
    {
        _skillText.text = $"Skill: {selectedSkill.GetSkillName()}";
    }

    private void OnDestroy() 
    {
        PlayerCombat.SkillChanged -= OnSkillChanged;
    }
}
