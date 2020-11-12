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

    private void Awake()
    {
        base.Bind<GameObject>(typeof(GameObjects));
    }

    public override void Init()
    {
        _guideText = Get<GameObject>((int)GameObjects.GuideText);
        _guideText.SetActive(false);

    }
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        Vector3 look = Camera.main.transform.position - transform.position;
        _guideText.transform.rotation = Camera.main.transform.rotation;
        NearNPC();

    }

    void NearNPC()
    {
        if (_scanObject != null)
        {
            //off 되어 있다면.
            if (!_guideText.active)
            {           
                _guideText.SetActive(true);
                _guideText.GetComponent<Text>().text = _speech;
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                _guideText.SetActive(false);
                _scanObject = null;
                Debug.Log("대화대화");
            }
        }
        else
        {
            _guideText.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("충돌");
        //만약 NPC가 앞에 있다면.
        Debug.Log($"other : {other.gameObject.layer} || {LayerMask.NameToLayer("NPC")}");
        if (other.gameObject.layer == LayerMask.NameToLayer("NPC"))
        {
            _speech = "대화하기 [F]";
            _scanObject = gameObject;
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
