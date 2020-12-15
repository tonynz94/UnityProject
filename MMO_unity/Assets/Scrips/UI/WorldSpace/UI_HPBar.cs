using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_HPBar : UI_Base
{
    protected enum GameObjects
    {
        HPBar,
        HPText,
    }

    protected Stat _stat;

    //부모클래스에서 실행해줌.
    public override void Init()
    {
        base.Bind<GameObject>(typeof(GameObjects));
        _stat = transform.parent.GetComponent<MonsterStat>();
    }

    protected virtual void Update()
    {
        Transform parent = gameObject.transform.parent;
        transform.position = parent.position + Vector3.up * (parent.GetComponent<Collider>().bounds.size.y);
        transform.rotation = Camera.main.transform.rotation;
       
        
        SetHPRatio();
    }

    public virtual void SetHPRatio()
    {
        float ratio = (float)_stat.Hp / _stat.MaxHp;
        GetObject((int)GameObjects.HPBar).GetComponent<Slider>().value = ratio;
    }
}
