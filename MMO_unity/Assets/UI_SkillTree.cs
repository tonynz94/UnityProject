using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_SkillTree : UI_Popup
{
    Text _skillPointText;
    UI_Skill_Tree[] _skillSlots;

    public enum GameObjects
    {
        MainSkillPointText
    }

    
    //스킬창을 킬 때.
    private void Awake()
    {
        Managers.Skill.OnLevelUpSkillUIUpdate -= UpdateSkillUI;
        Managers.Skill.OnLevelUpSkillUIUpdate += UpdateSkillUI;

        Bind<GameObject>(typeof(GameObjects));

        _skillPointText = Get<GameObject>((int)GameObjects.MainSkillPointText).GetComponent<Text>();
        _skillSlots = transform.GetComponentsInChildren<UI_Skill_Tree>();

        int skillId = 1000;
        foreach (UI_Skill_Tree skillSlot in _skillSlots)
        {
            skillSlot.SlotSkillId = skillId;
            skillId += 1000;
        }
        
    }

    public void Start()
    {
        UpdateSkillPointText();
        UpdateSkillUI(Managers.Skill._level);
    }

    public void UpdateSkillUI(int Level)
    {
        for (int i = 1000; (i / 1000) - 1 < Managers.Skill._totalSkillCount; i += 1000)
        {
            Debug.Log(Managers.Data.SkillDict[i].requireLevel);

            if (Managers.Data.SkillDict[i].requireLevel <= Level)
            {
                _skillSlots[(i/1000)-1].SkillOn();
            }
            else
            {
                _skillSlots[(i / 1000) - 1].SkillOff();
            }
        }
    }

    public void UpdateSkillPointText()
    {
        _skillPointText.text = $"SkillPoint : {SkillManager.SkillPoint}";
    }

    public override bool ClosePopupUI()
    {
        if (base.ClosePopupUI())
        {
            Managers.Skill.OnLevelUpSkillUIUpdate = null;
            return true;
        }
        return false;
    }

    public override string PopUpName()
    {
        return "SkillTree";
    }



}
