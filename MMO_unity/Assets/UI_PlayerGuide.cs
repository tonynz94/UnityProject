using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_PlayerGuide : UI_Base
{
    public GameObject _guideText;
    public string _speech;

    enum GameObjects
    {
        GuideText,
    }

    public override void Init()
    {
        base.Bind<GameObject>(typeof(GameObjects));
        _guideText = Get<GameObject>((int)GameObjects.GuideText);

        _guideText.GetComponent<Text>().text = _speech;

    }
    // Start is called before the first frame update
    public override void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        _guideText.transform.rotation = Camera.main.transform.rotation;
    }
}
