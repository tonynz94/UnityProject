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
        MPBar,
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
        //float EXPRatio = (float)_playerStat.Exp
        SetHPRatio(HPRatio);
    }

    public void SetHPRatio(float ratio)
    {
        GetObject((int)GameObjects.HPBar).GetComponent<Slider>().value = ratio;
    }

    public void SetEXPRatio(float ratio)
    {
        GetObject((int)GameObjects.HPBar).GetComponent<Slider>().value = ratio;
    }
}
