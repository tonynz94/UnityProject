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
    }

    public override void Init()
    {
        base.Bind<GameObject>(typeof(GameObjects));
        _player = Managers.Game.GetPlayer();
        _playerStat = _player.GetComponent<PlayerStat>();
    }

    // Update is called once per frame
    void Update()
    {
        float HPRatio = (float)_playerStat.Hp / _playerStat.MaxHp;
        float EXPRatio = ((float)_playerStat.Exp - Managers.Data.StatDict[_playerStat.Level - 1].totalExp )/ (Managers.Data.StatDict[_playerStat.Level].totalExp - Managers.Data.StatDict[_playerStat.Level - 1].totalExp);
        SetHPRatio(HPRatio);
        SetEXPRatio(EXPRatio);
    }

    public void SetHPRatio(float ratio)
    {
        GetObject((int)GameObjects.HPText).GetComponent<Text>().text = $"{_playerStat.Hp} / {_playerStat.MaxHp}";
        GetObject((int)GameObjects.HPBar).GetComponent<Slider>().value = ratio;
    }

    public void SetEXPRatio(float ratio)
    {
        GetObject((int)GameObjects.EXPText).GetComponent<Text>().text = $"{(float)_playerStat.Exp - Managers.Data.StatDict[_playerStat.Level - 1].totalExp} / {Managers.Data.StatDict[_playerStat.Level].totalExp - Managers.Data.StatDict[_playerStat.Level - 1].totalExp} [ {ratio * 100}% ]";
        GetObject((int)GameObjects.EXPBar).GetComponent<Slider>().value = ratio;
    }

    public void LevelUp(int level)
    {
        GetObject((int)GameObjects.LevelNumText).GetComponent<Text>().text = $"{level}";
    }
}
