using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_SkillButtons : UI_Base
{
    UI_SkillButton[] _skillButtons;
    public override void Init()
    {
        Managers.Skill.OnLevelUpSkilButtonUpdate -= LevelUpSkillOn;
        Managers.Skill.OnLevelUpSkilButtonUpdate += LevelUpSkillOn;
    }
    private void Awake()
    {
        Init();
        _skillButtons = transform.GetComponentsInChildren<UI_SkillButton>();
    }

    // Start is called before the first frame update
    void Start()
    {


    }

    void LevelUpSkillOn(int SkillId)
    {
        _skillButtons[(SkillId / 1000) - 1].SkillOn();
    }


}

