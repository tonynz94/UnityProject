using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStat : Stat
{
    [SerializeField]
    int _monsterExp;

    public GameObject DamageTxt;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        _monsterExp = 500;
        DamageTxt = Managers.Resource.Load<GameObject>("Prefabs/UI/WorldSpace/UI_DamageText");
    }


    //공격을 맞았을 때
    public override void OnAttacked(Stat attacker)
    {
        int damage = Mathf.Max(0, attacker.Attack - Defense);
        bool critical = false;

        //데미지 텍스트의 복사본을 가져옴.
        GameObject go = Object.Instantiate(DamageTxt, transform.position ,Camera.main.transform.rotation);


        if (Random.value < 0.7)
        {        
            critical = false;
        }
        else
        {
            damage *= 2;
            critical = true;
        }
        go.GetComponent<UI_DmgTxt>().SetDmgText(damage, critical);
         
        Hp -= damage;
        if (Hp < 0)
        {
            Hp = 0;
            OnDead(attacker);
        }
    }

    protected override void OnDead(Stat attacker)
    {
        Managers.Game.Despawn(gameObject);
        PlayerStat playerStat = attacker as PlayerStat;
        playerStat.Exp += _monsterExp;
    }
}
