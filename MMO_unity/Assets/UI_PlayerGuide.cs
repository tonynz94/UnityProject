using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_PlayerGuide : UI_Base
{
    GameObject _scanObject;

    public GameObject _guideText;
    string _speech;
    enum GameObjects
    {
        GuideText,
    }

    public override void Init()
    {
        base.Bind<GameObject>(typeof(GameObjects));
        _guideText = Get<GameObject>((int)GameObjects.GuideText);


        _guideText.SetActive(false);

    }
    // Start is called before the first frame update
    public override void Start()
    {

        base.Start();
        
    }

    // Update is called once per frame
    void Update()
    {
        _guideText.transform.rotation = Camera.main.transform.rotation;
        NearNPC();
    }

    void NearNPC()
    {
        //상대가 존재한다면.
        if (_scanObject != null)
        {
            //off 되어 있다면.
            if (!_guideText.active && !(Managers.Talk._isTalking))
            {           
                _guideText.SetActive(true);
                _guideText.GetComponent<Text>().text = _speech;
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                _guideText.SetActive(false);
                Managers.Talk._isTalking = !Managers.Talk._isTalking;

                if(Managers.Talk._isTalking)
                    Managers.Talk.TalkNPC(_scanObject , true); //대화창을 뛰움
                else
                    Managers.Talk.TalkNPC(null, false);
            }
        }
        else
        {
            _guideText.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("NPC"))
        {
            _speech = "대화하기 [F]";
            _scanObject = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("NPC"))
        {
            _scanObject = null;
        }
    }
}
