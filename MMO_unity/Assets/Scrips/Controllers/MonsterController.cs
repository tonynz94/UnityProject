using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class MonsterController : BaseController
{
    MonsterStat _stat;

    bool _hit = false;

    Rigidbody _rigidbody;

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
        _rigidbody = gameObject.GetComponent<Rigidbody>();
        _stat = gameObject.GetComponent<MonsterStat>();

        //최초 발결 된 것.
        if (gameObject.GetComponentInChildren<UI_HPBar>() == null)
            Managers.UI.MakeWorldSpaceUI<UI_HPBar>(transform);
    }

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
    }

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

    protected override void UpdateDie()
    {
        _died = true;
        StartCoroutine(OnDie());
    }

    protected IEnumerator OnDie()
    {
        State = Define.State.DIe;
        
        yield return new WaitForSeconds(2.0f);
        Managers.Game.Despawn(gameObject);
    }

    //때리는 함수
    void OnHitEvent()
    {
        if (_hit)
            return;
        if (_lockTarget != null)
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
                //몬스터 제자리로 가기.
            }
        } 
    }
    void FixedUpdate()
    {
           
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == (LayerMask.NameToLayer("SkillRange")))
        {
            Debug.Log($"other Name : {other.name}");
            StartCoroutine("coGetHit");
            gameObject.GetComponent<MonsterStat>().OnAttackedBySkill(other);

        }
    }

    IEnumerator coGetHit()
    {
        float exitTime = 0.9f;
        Animator anim = GetComponent<Animator>();
        State = Define.State.SkillHit;
        _hit = true;
        while (anim.GetCurrentAnimatorStateInfo(0).normalizedTime < exitTime)
        {
            yield return null;
        }
        State = Define.State.Idle;
        _hit = false;
    }
}
