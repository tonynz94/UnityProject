using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_SkillButton : UI_Base
{
    Image _coolDownImage;
    Image _skillImage;
    Text _coolDownText;
    GameObject _player;

    bool _isSkillOn;

    public float _coolDownTime;
    bool _isCoolDown;

    public Define.State _thisSkillState;
    public Define.SkillName _thisSkillName;
    enum GameObjects
    {
        CoolDownImage,
        CoolDownText,
        UI_SkillImage,
    }

    public void Awake()
    {
        Init();
    }

    public override void Init()
    {
        base.Bind<GameObject>(typeof(GameObjects));
    }

    // Start is called before the first frame update
    void Start()
    {
        _player = Managers.Game.GetPlayer();

        _skillImage = Get<GameObject>((int)GameObjects.UI_SkillImage).GetComponent<Image>();
        _coolDownImage = Get<GameObject>((int)GameObjects.CoolDownImage).GetComponent<Image>();     
        _coolDownText = Get<GameObject>((int)GameObjects.CoolDownText).GetComponent<Text>();
        
        _coolDownImage.fillAmount = 0.0f;
        _coolDownText.text = "";
        SkillOff();
    }

    public void Ability()
    {
        if (!_isSkillOn)
            return;

        //만약 쿨다운이면 스킬 사용이 안됨.
        if (_isCoolDown == true)
            return;

        //Mp 부족
        if (Managers.Game.GetPlayer().GetComponent<PlayerStat>().Mp - 20 < 0)
            return;
        
        Managers.Game.GetPlayer().GetComponent<PlayerStat>().Mp -= 20;
        int skillPoint = Managers.Skill.SkillListAndPoint[((int)_thisSkillName + 1) * 1000];
        float coolDown = Managers.Data.SkillDict[((int)_thisSkillName + 1) * 1000 + (skillPoint - 1)].coolDown;
        Debug.Log($"{((int)_thisSkillName + 1) * 1000 + (skillPoint - 1)}");


        StartCoroutine("coAbility", coolDown);
        ActionSkills(_thisSkillState);
    }

    IEnumerator coAbility(float coolDownTime)
    {
        float maxCoolDownTime = coolDownTime;
        float currentTime = coolDownTime;

        _isCoolDown = true;
        _coolDownImage.fillAmount = 1.0f;

        while (0.0f <= currentTime)
        {
            currentTime -= Time.deltaTime;          
            yield return null;
            _coolDownText.text = $"{(int)currentTime}";
            _coolDownImage.fillAmount = currentTime / maxCoolDownTime;
        }
        _coolDownText.text = "";
        _isCoolDown = false;

    }

    void ActionSkills(Define.State state)
    {
        Debug.Log($"{state}");
        if (state == Define.State.SkillQ)
            StartCoroutine(coQAction(state));
        else if (state == Define.State.SkillW)
            StartCoroutine(coWAction(state));
        else if (state == Define.State.SkillE)
            StartCoroutine(coEAction(state));
        else if (state == Define.State.SkillR)
            StartCoroutine(coRAction(state));
    }

    IEnumerator coQAction(Define.State state)
    {
        _player.GetComponent<PlayerController>().State = state;
        GameObject weapon = WeaponChange._equipedWeapon as GameObject;

        yield return new WaitForSeconds(2.0f);

    }

    IEnumerator coWAction(Define.State state)
    {
        _player.GetComponent<PlayerController>().State = state;
        GameObject weapon = WeaponChange._equipedWeapon as GameObject;

        _player.GetComponent<PlayerStat>().Attack += 30;
        
        yield return new WaitForSeconds(30.0f);

        _player.GetComponent<PlayerStat>().Attack -= 30;
    }

    IEnumerator coEAction(Define.State state)
    {
        _player.GetComponent<PlayerController>().State = state;
        GameObject weapon = WeaponChange._equipedWeapon as GameObject;

    
        yield return null;

        
    }

    IEnumerator coRAction(Define.State state)
    {
        _player.GetComponent<PlayerController>().State = state;
        GameObject weapon = WeaponChange._equipedWeapon as GameObject;

        yield return new WaitForSeconds(2.0f);
    }

    public void SkillOn()
    {
        _isSkillOn = true;
        _skillImage.color = new Color(_skillImage.color.r, _skillImage.color.g, _skillImage.color.b, 1.0f);
    }

    public void SkillOff()
    {
        _isSkillOn = false;
        Debug.Log(_skillImage);
        _skillImage.color = new Color(_skillImage.color.r, _skillImage.color.g, _skillImage.color.b, 0.3f);
    }

 

}
