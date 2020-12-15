using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStat : MonsterStat
{
    // Start is called before the first frame update
    void Start()
    {
        _level = 1;
        _hp = 5000;
        _mp = 100;
        _maxHp = 5000;
        _maxMp = 0;

        _attack = 300;
        _defense = 20;
        _critical = 0;
        _evasive = 0;

        DamageTxt = Managers.Resource.Load<GameObject>("Prefabs/UI/WorldSpace/UI_DamageText");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void OnDead(Stat attacker)
    {
        gameObject.GetComponent<BaseController>().State = Define.State.DIe;
    }
}
