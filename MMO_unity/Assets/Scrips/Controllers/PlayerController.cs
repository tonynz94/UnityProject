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
    public GameObject _guide;

    public GameObject SkillQRange;
    public GameObject SkillERange;
    public GameObject SkillRRange;

    public AudioSource __audioSources;

    float _radius = 1.0f;

    //수확 , 대화 관련 변수들
    UI_SpeechBox _speechBox;
    int _npcID;

    UI_Loading _LoadingBar;
    Animator _anim;

    public bool _isPressingF;
    public bool _isInAir;

    //Skills
    public List<UI_SkillButton> skillList = new List<UI_SkillButton>();

    public override Define.State State
    {
        get { return _state; }
        set
        {
            _state = value;
            string WeaponType;
            

            if(Managers.Equip.wearItems[0] == 0 ||
                Managers.Data.ItemDict[Managers.Equip.wearItems[0]].equipPart == "OneHandWeapon")
            {
                WeaponType = "OneHand";
            }
            else
                WeaponType = "TwoHand";

            switch (_state)
            {
                case Define.State.DIe:
                    //croofade 2번째 인자 => 어느정도 시간이 걸려서 넘어 갈것인지.           
                    break;
                case Define.State.Idle:
                    _anim.CrossFade($"{WeaponType}IDLE", 0.1f);
                    break;
                case Define.State.Moving:
                    _anim.CrossFade($"{WeaponType}RUN", 0.1f);
                    break;
                case Define.State.Skill:
                    //ATTACK에 0.1초의 진입시간, layer는 필요 없기에 -1, 마지막은 다시 하면 0(처음부터) 다시 실행 되는 것.
                    _anim.CrossFade($"{WeaponType}ATTACK", 0.1f, -1, 0);
                    break;
                case Define.State.Walk:
                    _anim.CrossFade("WALK", 0.1f);
                    break;
                case Define.State.SkillQ:
                    _anim.CrossFade($"{WeaponType}SKILL1", 0.1f, -1, 0);
                    break;
                case Define.State.SkillW:
                    _anim.CrossFade($"{WeaponType}SKILL2", 0.1f, -1, 0);
                    break;
                case Define.State.SkillE:
                    _anim.CrossFade($"{WeaponType}SKILL3", 0.1f, -1, 0);
                    break;
                case Define.State.SkillR:
                    _anim.CrossFade($"{WeaponType}SKILL4", 0.1f, -1, 0);
                    break;
            }
        }
    }

    public override void Init()
    {
        WorldObjectType = Define.WorldObject.Player;
        _guide = gameObject.transform.Find("UI_PlayerGuide").gameObject;
        _stat = gameObject.GetComponent<PlayerStat>();
        _rigid = gameObject.GetComponent<Rigidbody>();
        _nav = gameObject.GetComponent<NavMeshAgent>();
        __audioSources = gameObject.GetComponent<AudioSource>();
        _anim = GetComponent<Animator>();

        _guide.SetActive(false);
        Managers.Input.KeyAction -= OnKeyBoard;
        Managers.Input.KeyAction += OnKeyBoard;
        Managers.Input.MouseAction -= OnMouseEvent;
        Managers.Input.MouseAction += OnMouseEvent;
    }

    void Update()
    {
        base.Update();
    }

    protected override void UpdateMoving()
    {
        //True -> 공중에 있을 때
        //만약 캐릭터가 공중에 있다면 움직이지 못하도록 막아 줌.
        if (_isInAir)
            return;

        //_lockTarget은 몬스터를 클릭하면 해당 몬스터에 대한 게임오브젝트 정보가 저장 됨 
        //True -> 몬스터를 클릭하고 있다는 것. 거리를 계산하여 
        //False -> 땅을 클릭했다는 것.(이동)
        if(_lockTarget != null) 
        {
            //메인 캐릭터와 클릭한 몬스터의 거리를 측정합니다.
            //거리가 2이하면 공격을 실행 해 줌.
            _desPos = _lockTarget.transform.position;
            float distance = (_desPos - transform.position).magnitude;
            if(distance <= 2)
            {
                State = Define.State.Skill;
                return;
            }
        }

        //여기가 실행됬다는건 땅을 클릭했다는 것.
        Vector3 dir = _desPos - transform.position;
        dir.y = 0;

        //True -> 목적지(쿨릭한 땅)에 도착했을때 실행
        //상태를 Idle로 바꿔 줌.
        //False -> 목적지(클릭한 땅)에 도착하지 못했을 때 
        //이동
        if (dir.magnitude < 0.1f)
        {
            State = Define.State.Idle;
        }
        else
        {
            //메인 캐릭터로 부터 바라보는 방향으로 Raycast를 쏜다
            //만약 레이어가 Block이라는게 감지가 되면 메인 캐릭터를 Idle상태로 바꿔준다. 
            Debug.DrawRay(transform.position + Vector3.up * 0.5f, dir.normalized, Color.blue);
            if(Physics.Raycast(transform.position + Vector3.up * 0.5f, dir, 1.0f, LayerMask.GetMask("Block")))
            {
                if (Input.GetMouseButton(1) == false)
                    State = Define.State.Idle;
                return;
            }

            //캐릭터의 이동과 바라보는 방향을 설정해준다.
            float moveDist = Mathf.Clamp(_stat.MoveSpeed * Time.deltaTime, 0, dir.magnitude);
            transform.position += dir.normalized * moveDist;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir.normalized), 0.2f);
        }
    }

    protected override void UpdateSkill()
    {
        //공격하는 대상 바라보기.
        if(_lockTarget !=  null)
        {
            Vector3 dir = _lockTarget.transform.position - transform.position;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir.normalized), 20 * Time.deltaTime);
        }
    }

    //애니메이션에서 호출해주고 있음.
    void OnHitEvent()
    {
        if (_lockTarget != null)
        {
            Managers.Sound.Play("Sounds/GameSound/Beat1");
            Stat targetStat = _lockTarget.GetComponent<Stat>();
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

    //클릭 관련 이벤트가 발생하면 inputManager에서 델리게이트로 실행 하고 있음.
    void OnMouseEvent(Define.MouseEvent evt)
    {
        //캐릭터의 상태에 따른 함수 실행.
        switch (State)
        {
            case Define.State.Idle:
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
    }

    //캐릭터가 땅을 클릭할때 실행되는 함수.
    void OnMouseEvent_IdleRun(Define.MouseEvent evt)
    {
        //True ->무언가를 수확중일때 
        //수확중에 움직이면 수확하는 것을 멈춤.
        if(_LoadingBar != null)
            Managers.UI.ClosePopupUI(_LoadingBar);

        if (State == Define.State.DIe)
            return;

        //스크린에서 부터 Raycast를 쏴서 부딪친것을 hit에 저장시킴.
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        bool raycastHit = Physics.Raycast(ray, out hit, 100.0f, _mask);

        switch (evt)
        {       
            //최초로 마우스를 눌렀을때 실행.
            case Define.MouseEvent.PointerDown: 
                {
                    if (raycastHit)
                    {
                        //부딪친 오브젝트의 좌표를 가져 온 후
                        //공격을 하고 있었다면 멈추고 움직 임.
                        _desPos = hit.point;
                        State = Define.State.Moving;
                        _stopSkill = false;

                        //Raycast에 부딪친 좌표가 만약 몬스터라면 _LockTarget에 값을 넣어 줌
                        if (hit.collider.gameObject.layer == (int)Define.Layer.Monster)
                            _lockTarget = hit.collider.gameObject;              
                        else
                            _lockTarget = null;
                    }
                }
                break;
            //마우스를 누르고 있을때 실행.
            case Define.MouseEvent.Press:
                {
                    //실시간으로 부딪친 위치를 바꿔 줌.
                    if (_lockTarget == null && raycastHit)
                        _desPos = hit.point;
                }
                break;
            //땠을때 실행. 
            //공격하고 있었다면 공격을 멈춤
            case Define.MouseEvent.PointerUp:
                _stopSkill = true;
                break;
        }
    }

    void OnKeyBoard()
    {
        #region F_KeyDown
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (_scanObject == null)
                return;


            TurnOFFGuide();
            _npcID = _scanObject.GetComponent<ObjData>()._Id;

            if (_scanObject.layer == LayerMask.NameToLayer("NPC"))
            {
                _scanObject.GetComponent<ObjData>().Talking(true);
                Managers.Talk._isTalking = true;
                Managers.Talk.SpeakingNPCObject(_scanObject);
                Managers.Talk.SpeakWithNpc(_npcID);
            }
            else if (_scanObject.layer == LayerMask.NameToLayer("Collecting"))
            {
                _isPressingF = true;
                CollectingThings();
            }
        }
        #endregion
        if (Managers.Talk._isTalking)
        {
            return;
        }

        #region F_KeyUp
        if (Input.GetKeyUp(KeyCode.F))
        {
            if (_scanObject != null && _scanObject.layer == LayerMask.NameToLayer("Collecting"))
            {
                Managers.Talk._isTalking = false;
                _isPressingF = false;
                CollectingThings();
            }
        }
        #endregion

        //만약 말하고 있다면 스킬 사용 불가.
        if (WeaponChange._equipedWeapon != null)
        {
            if (Input.GetKeyDown(KeyCode.A))
                skillList[0].Ability();
            if (Input.GetKeyDown(KeyCode.S))
                skillList[1].Ability();
            if (Input.GetKeyDown(KeyCode.D))
                skillList[2].Ability();
            if (Input.GetKeyDown(KeyCode.G))
                skillList[3].Ability();
        }
    }

    void CollectingThings()
    {
        if (_LoadingBar == null)
        {
            _LoadingBar = Managers.UI.ShowPopupUI<UI_Loading>("UI_Loading");
            _LoadingBar.LoadingStart(4.0f, "수확 중");
            _anim.CrossFade("PICKING", 0.1f);
        }

        if (_isPressingF == false)
        {
            Managers.UI.ClosePopupUI(_LoadingBar);
            
        }
        else
        {
            if (_LoadingBar._isFinished)
            {
                Managers.UI.ClosePopupUI(_LoadingBar);
                //Managers.Inven.Add()              
                _isPressingF = false;
            }
        }

    }

    public void LoadingSuccess(bool isFinished)
    {
        if (isFinished)
        {
            Managers.Talk._isTalking = false;
            _isPressingF = false;
            Managers.UI.ClosePopupUI(_LoadingBar);
            Managers.Sound.Play("Sounds/GameSound/MissionCompleteCheck01");
            ObjData scanObject = _scanObject.GetComponent<ObjData>();

            if(scanObject._InteractionType == Define.InteractionType.Collecting)
                Managers.Inven.Add(scanObject._Id, Define.InvenType.Others);
            else
                Debug.Log("수확물이 아님");

            Destroy(scanObject.gameObject);
            Managers.Quest.IsCollectOrKill(scanObject._Id);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(gameObject.transform.position, _radius);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("NPC") ||
            other.gameObject.layer == LayerMask.NameToLayer("Collecting"))
        {     
            TurnOnGuide(other.gameObject.GetComponent<ObjData>().guideText);
            _scanObject = other.gameObject;
        }

        if(other.gameObject.layer == LayerMask.NameToLayer("BossSkill"))
        {
            StartCoroutine(coFourBack());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ((other.gameObject.layer == (LayerMask.NameToLayer("NPC")) ||
            (other.gameObject.layer == LayerMask.NameToLayer("Collecting"))))
        {
            TurnOFFGuide();
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
        yield return new WaitForSeconds(0.05f);
        _nav.enabled = false;
        _rigid.isKinematic = false; 
        _isInAir = true;
        _rigid.useGravity = true;
        Managers.Sound.Play("Sounds/GameSound/Hit");
        _rigid.AddRelativeForce(Vector3.back * 4, ForceMode.Impulse);
        _rigid.AddRelativeForce(Vector3.up * 5, ForceMode.Impulse);

        yield return new WaitForSeconds(0.9f);

        _isInAir = false;
        _nav.enabled = true;
        _rigid.isKinematic = true;
    }

    void TurnOnGuide(string guideText)
    {
        _guide.GetComponent<UI_PlayerGuide>().SetText(guideText);
        _guide.SetActive(true);
    }

    void TurnOFFGuide()
    {
        _guide.GetComponent<UI_PlayerGuide>().SetText("");
        _guide.SetActive(false);
    }

    //Sound

    void SoundWalkingStep()
    {
        __audioSources.volume = 1.0f;
       __audioSources.Play();
    }
}
