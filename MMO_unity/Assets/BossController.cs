using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossController : BaseController
{
    public bool isInBattleField;
    

    float MeleeAttackScanRange;
    public float _jumpPower;
    Rigidbody _rigid;
    GameObject _target;
    NavMeshAgent _navMeshAgent;

    public override Define.State State
    {
        get { return _state; }
        set
        {
            _state = value;

            Animator anim = GetComponent<Animator>();

            switch (_state)
            {
                case Define.State.DIe:
                    //croofade 2번째 인자 => 어느정도 시간이 걸려서 넘어 갈것인지.           
                    break;
                case Define.State.Idle:
                    anim.CrossFade("IDLE", 0.1f);
                    break;
                case Define.State.BossMeleeAttack:
                    //ATTACK에 0.1초의 진입시간, layer는 필요 없기에 -1, 마지막은 다시 하면 0(처음부터) 다시 실행 되는 것.
                    anim.CrossFade("MELEEATTACK", 0.1f, -1, 0);
                    break; 
                case Define.State.BossStoneSkill:
                    //ATTACK에 0.1초의 진입시간, layer는 필요 없기에 -1, 마지막은 다시 하면 0(처음부터) 다시 실행 되는 것.
                    anim.CrossFade("STONEATTACK", 0.1f, -1, 0);
                    break;
                case Define.State.BossJumpSkill:
                    //ATTACK에 0.1초의 진입시간, layer는 필요 없기에 -1, 마지막은 다시 하면 0(처음부터) 다시 실행 되는 것.
                    anim.CrossFade("JUMPATTACK", 0.1f, -1, 0);
                    break;
                case Define.State.SkillHit:
                    anim.CrossFade("HIT", 0.1f, -1, 0);
                    break;
            }

        }
    }



    public override void Init()
    {
        throw new System.NotImplementedException();
    }

    private void Awake()
    {
        MeleeAttackScanRange = 7.0f;
        _rigid = GetComponent<Rigidbody>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        isInBattleField = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        _jumpPower = 10.0f;
        _target = Managers.Game.GetPlayer();
        StartCoroutine(ThinkSkill());
    }

    // Update is called once per frame
    void Update()
    {
        if (isInBattleField)
        {
            transform.LookAt(_target.transform);
        }
    }

    IEnumerator ThinkSkill()
    {
        State = Define.State.Idle;
        GameObject player = Managers.Game.GetPlayer();
        yield return new WaitForSeconds(1.0f);

        float distance = (player.transform.position - transform.position).magnitude;

        if (distance <= MeleeAttackScanRange)
        {
            _lockTarget = player;
            StartCoroutine(coMeleeAttack());
        }
        else
        {
            //int ranAction = Random.Range(0, 1);
            int ranAction = 0;
            Debug.Log(ranAction);
            switch (ranAction)
            {
                case 0:
                    StartCoroutine(coJumpAttack());
                    break;
                case 1:
                    StartCoroutine(coStoneAttack());
                    break;
            }
        }
    }
    IEnumerator coMeleeAttack()
    {
        State = Define.State.BossMeleeAttack;
        yield return new WaitForSeconds(3.0f);

        StartCoroutine(ThinkSkill());
    }

    IEnumerator coJumpAttack()
    {
       
        State = Define.State.BossJumpSkill;

        yield return new WaitForSeconds(1.7f);
        _navMeshAgent.enabled = false;
       _rigid.AddForce(Vector3.up * _jumpPower, ForceMode.Impulse);

        //transform.Translate(_target.transform.position);
        yield return new WaitForSeconds(3.0f);
        _navMeshAgent.enabled = true;
        StartCoroutine(ThinkSkill());
    }

    IEnumerator coStoneAttack()
    {
        State = Define.State.BossStoneSkill;
        yield return new WaitForSeconds(3.0f);

        StartCoroutine(ThinkSkill());
    }

}
