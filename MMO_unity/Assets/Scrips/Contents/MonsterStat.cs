using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStat : Stat
{
    [SerializeField]
    int _monsterExp;
    
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        _monsterExp = 500;
    }


    //공격을 맞았을 때
    public override void OnAttacked(Stat attacker)
    {
        if (gameObject.GetComponent<BaseController>()._died)
            return;

        if (gameObject.GetComponent<BaseController>().State == Define.State.DIe)
            return;

        int damage = Mathf.Max(0, attacker.Attack - Defense);
        bool critical = false;

        //데미지 텍스트의 복사본을 가져옴.
        GameObject go = Object.Instantiate(DamageTxt, transform.position ,Camera.main.transform.rotation);

        if (Random.value < attacker.Critical *0.01f)
        {
            damage *= 2;
            critical = true;
        }
        else
        {
            critical = false;
        }

        go.GetComponent<UI_DmgTxt>().SetDmgText(damage, critical, false, Color.yellow);
         
        Hp -= damage;
        if (Hp < 0)
        {
            Hp = 0;
            Debug.Log("몬스터 죽음@@");
            OnDead(attacker);
        }
    }

    //스킬로 공격을 맞았을때.
    public void OnAttackedBySkill(Collider skillName)
    {
        if (gameObject.GetComponent<BaseController>()._died)
            return;

        Define.SkillName _skill = skillName.GetComponent<PlayerSkill>()._skillName;
        Debug.Log((int)_skill);

        //스킬스텟을 가져와야함.
        int skillId = ((int)_skill + 1)*1000;
        skillId = (Managers.Skill.SkillListAndPoint[skillId]-1) + skillId;
        Debug.Log(skillId);

        int damage = (int)Mathf.Max(0, Managers.Game.GetPlayer().GetComponent<PlayerStat>().Attack * Managers.Data.SkillDict[skillId].skillDamage - Defense);
        bool critical = false;

        if (Random.value < Managers.Game.GetPlayer().GetComponent<PlayerStat>().Critical * 0.01f)
        {
            damage *= 2;
            critical = true;
        }
        else
        {
            critical = false;
        }

        GameObject go = Object.Instantiate(DamageTxt, transform.position, Camera.main.transform.rotation);
        go.GetComponent<UI_DmgTxt>().SetDmgText(damage, critical, false, Color.yellow);

        Hp -= damage;
        if (Hp < 0)
        {
            Hp = 0;
            Debug.Log("몬스터 죽음@@");
            OnDead(Managers.Game.GetPlayer().GetComponent<PlayerStat>());
        }

        Debug.Log(damage);
    }

    


    protected override void OnDead(Stat attacker)
    {
        gameObject.GetComponent<BaseController>().State = Define.State.DIe;
        PlayerStat playerStat = attacker as PlayerStat;
        playerStat.Exp += _monsterExp;
    }
}
