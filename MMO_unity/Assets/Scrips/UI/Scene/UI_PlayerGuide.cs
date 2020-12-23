using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_PlayerGuide : UI_Base
{
    public GameObject _guideText;

    enum GameObjects
    {
        GuideText,
    }

    public override void Init()
    {
        base.Bind<GameObject>(typeof(GameObjects));
        _guideText = Get<GameObject>((int)GameObjects.GuideText);
    }
    // Start is called before the first frame update
    public void Awake()
    {
        Init();
    }

    public override void Start()
    {

    }

    // Update is called once per frame

    void Update()
    {
        _guideText.transform.rotation = Camera.main.transform.rotation;
    }

    public void SetText(string guideText)
    {
        _guideText.GetComponent<Text>().text = guideText;
    }

}
