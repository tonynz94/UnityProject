using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossController : MonsterController
{
    public bool _isLooking;
    public bool _isInBattleField;
    public bool _isInAir;


    float MeleeAttackScanRange;
    float movePerSecond;
    float distance;


    public float _jumpPower;

    Vector3 dir;
    Vector3 _jumpPosition;

    Rigidbody _rigid;
    GameObject _target;
    GameObject _meleeAttackArea;
    GameObject _jumpAttackArea;
    GameObject _stoneSpawnArea;
    
    GameObject _stone;
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
                    anim.CrossFade("DIE", 0.1f);
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
        _isInBattleField = false;
        _isInAir = false;
        _isLooking = true;

        _meleeAttackArea = transform.Find("MeleeAttackArea").gameObject;
        _jumpAttackArea = transform.Find("JumpArea").gameObject;
        _stoneSpawnArea = transform.Find("StoneSpawnArea").gameObject;
        _stone = Managers.Resource.Load<GameObject>("");

        _meleeAttackArea.SetActive(false);
        _jumpAttackArea.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {     
        _jumpPower = 10.0f;
        _target = Managers.Game.GetPlayer();     
    }

    public void StartBossStage()
    {
        _isInBattleField = true;
        StartCoroutine(ThinkSkill());
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (!_isInBattleField)
            return;

        if (State == Define.State.DIe)
            return;

        //플레이어가 근처에 있는가??
        if (_isLooking)
        {
            Vector3 dir = (_target.transform.position - transform.position).normalized;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir.normalized), 0.2f);
        }
        else
        {
            float distance = (_jumpPosition - transform.position).magnitude;
            float speed = Mathf.Clamp(movePerSecond * Time.deltaTime, 0, distance);
           
            if(distance < 1.0f)
            {
                _isLooking = true;
            }
            else
            {
                this.transform.position += dir * (speed);
            }
        }
    }

    IEnumerator ThinkSkill()
    {
        if (State == Define.State.DIe)
            yield break;

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
            int ranAction = Random.Range(0, 2);
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
        yield return new WaitForSeconds(1.5f);
        _meleeAttackArea.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        _meleeAttackArea.SetActive(false);
        yield return new WaitForSeconds(1.3f);

        StartCoroutine(ThinkSkill());
    }

    IEnumerator coJumpAttack()
    {

        State = Define.State.BossJumpSkill;

        _jumpPosition = _target.transform.position;
        distance = (_jumpPosition - transform.position).magnitude;    
        dir = (_jumpPosition - transform.position).normalized;

        movePerSecond = distance / 2.0f;
        _isInAir = true;
        yield return new WaitForSeconds(1.7f);
        _isLooking = false;
        //점프 직후
        _navMeshAgent.enabled = false;
        _rigid.isKinematic = false;
        
        _rigid.AddForce(Vector3.up * _jumpPower, ForceMode.Impulse);
        _jumpAttackArea.SetActive(true);
        
        yield return new WaitForSeconds(2.0f);
        //착지 직후
        _isLooking = true;
        _navMeshAgent.enabled = true;
        _rigid.isKinematic = true;
        State = Define.State.Idle;
        _jumpAttackArea.SetActive(false);

        yield return new WaitForSeconds(1.0f);
        
        StartCoroutine(ThinkSkill());
    }

    IEnumerator coStoneAttack()
    {
        State = Define.State.BossStoneSkill;
        GameObject go= Instantiate(Managers.Resource.Load<GameObject>($"Prefabs/Stone"),
                                                    _stoneSpawnArea.transform.position,
                                                    _stoneSpawnArea.transform.rotation);
        yield return new WaitForSeconds(3.0f);

        StartCoroutine(ThinkSkill());
    }

    IEnumerator CheckIfSkillisFinish()
    {
        float exitTime = 0.9f;
        Animator anim = GetComponent<Animator>();

        while (anim.GetCurrentAnimatorStateInfo(0).normalizedTime < exitTime)
        {
            yield return null;
        }

        State = Define.State.Idle;
    }

    protected void BossDie()
    {
        StartCoroutine(OnDie());
    }

    protected IEnumerator OnDie()
    {
        yield return new WaitForSeconds(2.0f);
        Managers.Game.Despawn(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("MainPlayer"))
        {
            _jumpAttackArea.SetActive(false);
            _meleeAttackArea.SetActive(false);
        }

        if (other.gameObject.layer == (LayerMask.NameToLayer("SkillRange")))
        {
            gameObject.GetComponent<BossStat>().OnAttackedBySkill(other);
        }
    }

}
