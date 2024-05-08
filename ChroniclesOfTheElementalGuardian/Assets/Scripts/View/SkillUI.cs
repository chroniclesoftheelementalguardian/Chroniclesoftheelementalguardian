using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SkillUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _skillText;

    [SerializeField] private List<SkillSprites> skillSprites = new List<SkillSprites>();
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
        SelectSkillSprite(selectedSkill);
        _skillText.text = $"{selectedSkill.GetSkillName()}";
    }

    private void SelectSkillSprite(Skill selectedSkill)
    {
        for (int i = 0; i < skillSprites.Count; i++)
        {
            if(skillSprites[i].skillName == selectedSkill.GetSkillName())
            {
                skillSprites[i].skillGameobject.SetActive(true);
            }
            else
            {
                skillSprites[i].skillGameobject.SetActive(false);
            }
            
        }
    }

    private void OnDestroy() 
    {
        PlayerCombat.SkillChanged -= OnSkillChanged;
    }

    [Serializable]
    public class SkillSprites
    {
        [SerializeField] public GameObject skillGameobject;
        [SerializeField] public String skillName;
    }
}
