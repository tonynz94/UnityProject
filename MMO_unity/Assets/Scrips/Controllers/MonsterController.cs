using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController : BaseController
{
    MonsterStat _stat;

    [SerializeField]
    float _scanRange = 10;

    [SerializeField]
    float _attackRange = 2;

    [SerializeField]
    Vector3 _firstSpawnPoint;



    int movementFlag = 0; //0 Idle , 1 move

    public override void Init()
    {
        WorldObjectType = Define.WorldObject.Monster;
        _stat = gameObject.GetComponent<MonsterStat>();

        //최초 발결 된 것.
        if (gameObject.GetComponentInChildren<UI_HPBar>() == null)
            Managers.UI.MakeWorldSpaceUI<UI_HPBar>(transform);

    }

/*
    IEnumerator NonTargetChange()
    {
        movementFlag = Random.Range(0, 2);

        if (movementFlag == 0)
        {
            State = Define.State.Idle;
        }
        else if (movementFlag == 1)
        {
            State = Define.State.Walk;
            _desPos = Random.insideUnitSphere * 3.0f + _firstSpawnPos;
            see = _desPos;
            _desPos.y = 0;
        }

        yield return new WaitForSeconds(3.0f);


        StartCoroutine("NonTargetChange");
    }

        */

    protected override void UpdateIdle()
    {
        GameObject player = Managers.Game.GetPlayer();
        //player가 유효한지
        if (player.isValid() == false)
            return;

        float distance = (player.transform.position - transform.position).magnitude;

        if(distance <= _scanRange)
        {
            _lockTarget = player;
            State = Define.State.Moving;
            return;
        }
        //근처에 공격할 대상이 없을 시.     
    }

/*
    protected override void UpdateWalk()
    {
        //타켓이 없을 시 실행.
        Vector3 dir = (_desPos - transform.position).normalized;
        transform.position += dir * Time.deltaTime * 10.0f;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 0.2f);
    }
    */



    protected override void UpdateMoving()
    {
        if (_lockTarget != null)
        {
            _desPos = _lockTarget.transform.position;
            float distance = (_desPos - transform.position).magnitude;
            //만약 공격 범위 안에 있다면.
            if (distance <= _attackRange)
            {
                float moveDist = Mathf.Clamp(_stat.MoveSpeed * Time.deltaTime, 0, (_desPos - transform.position).magnitude);
                transform.position += (_desPos - transform.position).normalized * moveDist;
                State = Define.State.Skill;
                return;
            }
            //거리가 많이 멀어졌다면.
            else if(distance > _scanRange)
            {
                _lockTarget = null;

                //이때 다시 원위치로 돌아가기.
            }
        }

        Vector3 dir = _desPos - transform.position;
        if(_lockTarget == null)
        {
            State = Define.State.Idle;
        }
        else
        {
            float moveDist = Mathf.Clamp(_stat.MoveSpeed * Time.deltaTime, 0, (_desPos - transform.position).magnitude);
            transform.position += (_desPos - transform.position).normalized * moveDist;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir.normalized), 0.2f);
        }
    }

    protected override void UpdateSkill()
    {
        //공격하는 대상 바라보기.
        if (_lockTarget != null)
        {
            Vector3 dir = _lockTarget.transform.position - transform.position;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir.normalized), 20 * Time.deltaTime);
        }
    }

    //때리는 함수
    void OnHitEvent()
    {
        if(_lockTarget != null)
        {
            Stat targetStat = _lockTarget.GetComponent<PlayerStat>();
            targetStat.OnAttacked(_stat);

            if (targetStat.Hp > 0)
            {
                float distance = (_lockTarget.transform.position - transform.position).magnitude;
                if (distance <= _attackRange)
                    State = Define.State.Skill;
                else
                    State = Define.State.Moving;
            }
            else
            {

            }
        } 
    }
}
