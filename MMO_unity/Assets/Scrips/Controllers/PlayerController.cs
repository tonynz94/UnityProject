using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : BaseController
{
    int _mask = (1 << (int)Define.Layer.Ground) | (1 << (int)Define.Layer.Monster);

    public PlayerStat _stat;

    //퀘스트 QuestManager에 우선적으로 코딩 만약 완성하면 밑에 지워
    public List<Data.Quest> quest = new List<Data.Quest>();
    
    
    bool _stopSkill = false;

    Rigidbody _rigid;
    NavMeshAgent _nav;

    public GameObject _scanObject = null;
    public GameObject _speekingNPC;
    public GameObject _guideText;

    public GameObject SkillQRange;
    public GameObject SkillERange;
    public GameObject SkillRRange;

    float _radius = 1.0f;

    //수확 , 대화 관련 변수들
    UI_SpeechBox _speechBox;
    public bool _isTalking;
    int _npcID;

    UI_Loading _LoadingBar;
    public bool _isPressingF;


    //Skills
    public List<UI_SkillButton> skillList = new List<UI_SkillButton>();

    public override Define.State State
    {
        get { return _state; }
        set
        {
            _state = value;
            string WeaponType;
            Animator anim = GetComponent<Animator>();

            if(Managers.Equip.wearItems[0] == 0 ||
                Managers.Data.ItemDict[Managers.Equip.wearItems[0]].equipPart == "OneHandWeapon")
            {
                WeaponType = "OneHand";
            }
            else
                WeaponType = "TwoHand";


            Debug.Log($"{WeaponType}");
            switch (_state)
            {
                case Define.State.DIe:
                    //croofade 2번째 인자 => 어느정도 시간이 걸려서 넘어 갈것인지.           
                    break;
                case Define.State.Idle:
                    anim.CrossFade($"{WeaponType}IDLE", 0.1f);
                    break;
                case Define.State.Moving:
                    anim.CrossFade($"{WeaponType}RUN", 0.1f);
                    break;
                case Define.State.Skill:
                    //ATTACK에 0.1초의 진입시간, layer는 필요 없기에 -1, 마지막은 다시 하면 0(처음부터) 다시 실행 되는 것.
                    anim.CrossFade($"{WeaponType}ATTACK", 0.1f, -1, 0);
                    break;
                case Define.State.Walk:
                    anim.CrossFade("WALK", 0.1f);
                    break;
                case Define.State.SkillQ:
                    anim.CrossFade($"{WeaponType}SKILL1", 0.1f, -1, 0);
                    break;
                case Define.State.SkillW:
                    anim.CrossFade($"{WeaponType}SKILL2", 0.1f, -1, 0);
                    break;
                case Define.State.SkillE:
                    anim.CrossFade($"{WeaponType}SKILL3", 0.1f, -1, 0);
                    break;
                case Define.State.SkillR:
                    anim.CrossFade($"{WeaponType}SKILL4", 0.1f, -1, 0);
                    break;
            }
        }
    }

    public override void Init()
    {
        WorldObjectType = Define.WorldObject.Player;

        _stat = gameObject.GetComponent<PlayerStat>();
        _rigid = gameObject.GetComponent<Rigidbody>();
        _nav = gameObject.GetComponent<NavMeshAgent>();

        Managers.Input.KeyAction -= OnKeyBoard;
        Managers.Input.KeyAction += OnKeyBoard;
        Managers.Input.MouseAction -= OnMouseEvent;
        Managers.Input.MouseAction += OnMouseEvent;
    }

    void Update()
    {
        base.Update();
        /*float distance = transform.position - gameObject.transform.position).magnitude;
        if (distance <= radius)
        {
            _player.GetComponent<PlayerController>()._scanObject = gameObject;
        }
        else
        {
            _player.GetComponent<PlayerController>()._scanObject = null;
        }*/

    }

    protected override void UpdateMoving()
    {
        //타켓이 있을시 몬스터가 내 사정거리보다 가까우면 공격

        if(_lockTarget != null) 
        {
            //자신과 클릭된 적의 거리
            //이게 없으면 공중에 찍어서 1의 값이 넘어가서 가까이 접근을 못하는 상황이 발생
            _desPos = _lockTarget.transform.position;
            float distance = (_desPos - transform.position).magnitude;
            if(distance <= 1)
            {
                State = Define.State.Skill;
                return;
            }
        }

        Vector3 dir = _desPos - transform.position;
        dir.y = 0;

        if (dir.magnitude < 0.1f)
        {
            //이 함수를 실행을 멈춤.
            State = Define.State.Idle;
        }
        else
        {
            //TODO
         
            //특점 지점으로 갈 수 있는지에 대한 여부
            //nma.CalculatePath
           
            //포지션으로 바로 움직여주는 것이 아닌 navmasAgent로 움직임.
            //정밀도 있게 원하는곳으로 가주지는 않음. 0.0001에서 0.1로 바꿔줘야함.
            //충돌처리
            Debug.DrawRay(transform.position + Vector3.up * 0.5f, dir.normalized, Color.blue);
            if(Physics.Raycast(transform.position + Vector3.up * 0.5f, dir, 1.0f, LayerMask.GetMask("Block")))
            {
                if (Input.GetMouseButton(1) == false)
                    State = Define.State.Idle;
                return;
            }
            //transform.position += dir.normalized * moveDist;
            float moveDist = Mathf.Clamp(_stat.MoveSpeed * Time.deltaTime, 0, dir.magnitude);
            transform.position += dir.normalized * moveDist;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir.normalized), 0.2f);
        }
    }

    protected override void UpdateSkill()
    {
        //공격하는 대상 바라보기.
        if(_lockTarget != null)
        {
            Vector3 dir = _lockTarget.transform.position - transform.position;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir.normalized), 20 * Time.deltaTime);
        }
    }

    //애니메이션에서 호출해주고 있음.
    void OnHitEvent()
    {
        if(_lockTarget != null)
        {         
            Stat targetStat = _lockTarget.GetComponent<MonsterStat>();
            targetStat.OnAttacked(_stat);
        }

        if(_stopSkill)
        {
            State = Define.State.Idle;
        }
        else
        {
            State = Define.State.Skill;
        }
    }

    //클릭 관련 이벤트가 발생하면 inputManager에서 델리게이트로 실행함.
    void OnMouseEvent(Define.MouseEvent evt)
    {
        switch (State)
        {
            case Define.State.Idle:
                OnMouseEvent_IdleRun(evt);
                break;
            case Define.State.Moving:
                OnMouseEvent_IdleRun(evt);
                break;
            case Define.State.Skill:
                {
                    if(evt == Define.MouseEvent.PointerUp)
                        _stopSkill = true;
                }
                break;
            case Define.State.SkillQ:
            case Define.State.SkillW:
            case Define.State.SkillE:
            case Define.State.SkillR:
                {
                    StartCoroutine(CheckIfSkillisFinish());
                }
                break;
        }
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
        Debug.Log(_stopSkill);
    }

    void OnMouseEvent_IdleRun(Define.MouseEvent evt)
    {
        if (State == Define.State.DIe)
            return;

        RaycastHit hit;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        bool raycastHit = Physics.Raycast(ray, out hit, 100.0f, _mask);

        //Debug.DrawRay(Camera.main.transform.position, ray.direction * 10, Color.red, 1.0f);
        switch (evt)
        {       
            case Define.MouseEvent.PointerDown: //마우스를 뗀 상태에서 처음으로 딱 누른 상태.
                {
                    if (raycastHit)
                    {
                        //부딪친 좌표
                        _desPos = hit.point;
                        State = Define.State.Moving;
                        _stopSkill = false;

                        if (hit.collider.gameObject.layer == (int)Define.Layer.Monster)
                            _lockTarget = hit.collider.gameObject;              
                        else
                            _lockTarget = null;
                    }
                }
                break;
            case Define.MouseEvent.Press:
                {
                    if (_lockTarget == null && raycastHit)
                        _desPos = hit.point;
                }
                break;
            case Define.MouseEvent.PointerUp:
                _stopSkill = true;
                break;
        }
    }

    void OnKeyBoard()
    { 
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("F 키를 눌름 ");
            if (_scanObject == null)
                return;

            
            _guideText.SetActive(false);
            _npcID = _scanObject.GetComponent<ObjData>()._NpcId;

            if (_scanObject.layer == LayerMask.NameToLayer("NPC"))
            {
                _isTalking = true;
                Managers.Talk.SpeakWithNpc(_npcID);
            }
            
            
            else if (_scanObject.layer == LayerMask.NameToLayer("Collecting"))
            {
                _isPressingF = true;
                CollectingThings();
            }
        }

        if (Input.GetKeyUp(KeyCode.F))
        {
            Debug.Log("키를 땜");
            _isTalking = false;
            _isPressingF = false;
            CollectingThings();
        }

        //만약 말하고 있다면 스킬 사용 불가.
        if (_isTalking)
            return;
        if (WeaponChange._equipedWeapon != null)
        {
            if (Input.GetKeyDown(KeyCode.Q))
                skillList[0].Ability();
            if (Input.GetKeyDown(KeyCode.W))
                skillList[1].Ability();
            if (Input.GetKeyDown(KeyCode.E))
                skillList[2].Ability();
            if (Input.GetKeyDown(KeyCode.R))
                skillList[3].Ability();
        }
    }

    void CollectingThings()
    {

        if (_LoadingBar == null)
        {
            _LoadingBar = Managers.UI.ShowPopupUI<UI_Loading>("UI_Loading");
            _LoadingBar.LoadingStart(4.0f, "수확 중");
        }

        Debug.Log("수확 중");
        if (_isPressingF == false)
        {
            Debug.Log("수확실패");         
            Managers.UI.ClosePopupUI(_LoadingBar);
        }
        else
        {
            if (_LoadingBar._isFinished)
            {
                Managers.UI.ClosePopupUI(_LoadingBar);
                Debug.Log("수확성공");
            }
        }
    }

    public void LoadingSuccess(bool isFinished)
    {
        if (isFinished)
        {
            _isTalking = false;
            _isPressingF = false;
            Managers.UI.ClosePopupUI(_LoadingBar);
            Debug.Log("수확 성공");
            Debug.Log("해당 수확한 아이템 인벤토리에 추가");
            Debug.Log("Quest 검사");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(gameObject.transform.position, _radius);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("NPC"))
        {    
            _guideText.SetActive(true);
            _guideText.GetComponent<UI_PlayerGuide>()._speech = "대화하기 [F]";
            _scanObject = other.gameObject;
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("Collecting"))
        {         
            _guideText.SetActive(true);
            _guideText.GetComponent<UI_PlayerGuide>()._speech = "수확하기 [F]";
            _scanObject = other.gameObject;
        }

        if(other.gameObject.name == "JumpAttackArea")
        {
            Debug.Log("넉백");
            StartCoroutine(coFourBack());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ((other.gameObject.layer == (LayerMask.NameToLayer("NPC")) ||
            (other.gameObject.layer == LayerMask.NameToLayer("Collecting"))))
        {
            _guideText.SetActive(false);
            _scanObject = null;
        }
    }

    public void SkillQColliderOn()
    {
        SkillQRange.SetActive(true);
    }

    public void SkillQColliderOff()
    {
        SkillQRange.SetActive(false);
    }

    public void SkillRColliderOn()
    {
        SkillRRange.SetActive(true);
    }

    public void SkillRColliderOff()
    {
        SkillRRange.SetActive(false);
    }

    IEnumerator coFourBack()
    {
        _nav.enabled = false;
        _rigid.isKinematic = false;

        _rigid.useGravity = true;
        _rigid.AddForce(Vector3.back * 2, ForceMode.Impulse);
        _rigid.AddForce(Vector3.up * 3, ForceMode.Impulse);

        yield return new WaitForSeconds(1.3f);

        _nav.enabled = true;
        _rigid.isKinematic = true;
    }
}
