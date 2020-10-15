using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_HPBar : UI_Base
{
    enum GameObjects
    {
        HPBar,
    }

    Stat _stat;

    //부모클래스에서 실행해줌.
    public override void Init()
    {
        base.Bind<GameObject>(typeof(GameObjects));
        _stat = transform.parent.GetComponent<Stat>(); 
    }

    private void Update()
    {
        Transform parent = gameObject.transform.parent;
        transform.position = parent.position + Vector3.up * (parent.GetComponent<Collider>().bounds.size.y);
        transform.rotation = Camera.main.transform.rotation;

        float ratio = (float)_stat.Hp / _stat.MaxHp;
        SetHPRatio(ratio);
    }

    public void SetHPRatio(float ratio)
    {
        GetObject((int)GameObjects.HPBar).GetComponent<Slider>().value = ratio;
    }
}
