using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_BossHPBar : UI_HPBar
{
    GameObject _hpText;
    GameObject _hpBar;

    public override void Init()
    {
        base.Bind<GameObject>(typeof(GameObjects));
        _stat = GameObject.Find("Boss").GetComponent<BossStat>();
        _hpText = Get<GameObject>((int)GameObjects.HPText);
    }
    // Update is called once per frame
    protected override void Update()
    {    
        SetHPRatio();
    }

    public override void SetHPRatio()
    {
        base.SetHPRatio();
        _hpText.GetComponent<Text>().text = $"{_stat.Hp}/{_stat.MaxHp}";
    }
}
