using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_SkillButton : UI_Base
{
    Image _coolDownImage;
    Text _coolDownText;
    Animator anim;

    public float _coolDownTime;
    bool _isCoolDown;

    enum GameObjects
    {
        CoolDownImage,
        CoolDownText,
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
        anim = Managers.Game.GetPlayer().GetComponent<Animator>();
        _coolDownImage = Get<GameObject>((int)GameObjects.CoolDownImage).GetComponent<Image>();
        _coolDownText = Get<GameObject>((int)GameObjects.CoolDownText).GetComponent<Text>();

        _coolDownImage.fillAmount = 0.0f;
        _coolDownText.text = "";
    }

    public void Ability(float coolDownTime)
    {
        //만약 쿨다운이면 스킬 사용이 안됨.
        if (_isCoolDown == true)
            return;

        StartCoroutine("coAbility", coolDownTime);
        StartCoroutine("coAction", coolDownTime);
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

    IEnumerator coAction(float coolDownTime)
    {
        //anim.CrossFade("SkillQ", 0.1f);
        yield return new WaitForSeconds(2.0f);
    }
}
