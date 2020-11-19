using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_PlayerHPBar : UI_Base
{
    public GameObject _player;
    PlayerStat _playerStat;

    enum GameObjects
    {
        HPBar,
        HPText,
        MPBar,
        MPText,
        EXPBar,
        EXPText,
        LevelNumText,
        LevelText,

        BackGroundQ,
        BackGroundW,
        BackGroundE,
        BackGroundR,
    }

    public override void Init()
    {
        base.Bind<GameObject>(typeof(GameObjects));
        _player = Managers.Game.GetPlayer();

        //skill Attach
        _player.GetComponent<PlayerController>().skillList.Add(Get<GameObject>((int)GameObjects.BackGroundQ).GetComponent<UI_SkillButton>());
        _player.GetComponent<PlayerController>().skillList.Add(Get<GameObject>((int)GameObjects.BackGroundW).GetComponent<UI_SkillButton>());
        _player.GetComponent<PlayerController>().skillList.Add(Get<GameObject>((int)GameObjects.BackGroundE).GetComponent<UI_SkillButton>());
        _player.GetComponent<PlayerController>().skillList.Add(Get<GameObject>((int)GameObjects.BackGroundR).GetComponent<UI_SkillButton>());

        _playerStat = _player.GetComponent<PlayerStat>();

    }

    // Update is called once per frame
    void Update()
    {
        float HPRatio = (float)_playerStat.Hp / _playerStat.MaxHp;
        float MPRatio = (float)_playerStat.Mp / _playerStat.MaxMp;
        float EXPRatio = ((float)_playerStat.Exp - Managers.Data.StatDict[_playerStat.Level - 1].totalExp )/ (Managers.Data.StatDict[_playerStat.Level].totalExp - Managers.Data.StatDict[_playerStat.Level - 1].totalExp);
        SetHPRatio(HPRatio);
        SetMPRatio(MPRatio);
        SetEXPRatio(EXPRatio);
    }

    public void SetHPRatio(float ratio)
    {
        GetObject((int)GameObjects.HPText).GetComponent<Text>().text = $"{_playerStat.Hp} / {_playerStat.MaxHp}";
        GetObject((int)GameObjects.HPBar).GetComponent<Slider>().value = ratio;
    }

    public void SetMPRatio(float ratio)
    {
        GetObject((int)GameObjects.MPText).GetComponent<Text>().text = $"{_playerStat.Mp} / {_playerStat.MaxMp}";
        GetObject((int)GameObjects.MPBar).GetComponent<Slider>().value = ratio;
    }

    public void SetEXPRatio(float ratio)
    {
        double expValue = ratio * 100;
        expValue = Math.Truncate(expValue * 100) / 100;
        GetObject((int)GameObjects.EXPText).GetComponent<Text>().text = $"{(float)_playerStat.Exp - Managers.Data.StatDict[_playerStat.Level - 1].totalExp} / {Managers.Data.StatDict[_playerStat.Level].totalExp - Managers.Data.StatDict[_playerStat.Level - 1].totalExp} [ {expValue}% ]";
        GetObject((int)GameObjects.EXPBar).GetComponent<Slider>().value = ratio;
    }

    public void LevelUp(int level)
    {
        Text h = GetObject((int)GameObjects.LevelNumText).GetComponent<Text>();
        h.text = $"{level}";
    }
}
