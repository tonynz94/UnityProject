﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Loading : UI_Popup
{
    // Start is called before the first frame update
    PlayerController _playerController;

    Slider _LoadingBar;
    Text _LoadingText;
    
    public bool _isFinished;

    enum GameObjects
    {
        LoadingText,
        LoadingBar,
    }

    private void Awake()
    {
        base.Bind<GameObject>(typeof(GameObjects));
        Init();
    }

    public void Init()
    {
        _playerController = Managers.Game.GetPlayer().GetComponent<PlayerController>();
        _LoadingBar = Get<GameObject>((int)GameObjects.LoadingBar).GetComponent<Slider>();
        _LoadingText = Get<GameObject>((int)GameObjects.LoadingText).GetComponent<Text>();
        _isFinished = false;
        
    }

    public void LoadingStart(float maxWaitTime, string loadingText)
    {
        _LoadingText.text = loadingText;
        StartCoroutine("coLoadingStart", maxWaitTime);
    }

    IEnumerator coLoadingStart(float maxWaitTime)
    {
        float currentTime = 0;
        Managers.Sound.Play("Sounds/GameSound/Collecting");
        while (_LoadingBar.value != 1.0f)
        {
            currentTime += Time.deltaTime;
            _LoadingBar.value = currentTime / maxWaitTime;
            yield return null;

        }
        _isFinished = true;
        _playerController.LoadingSuccess(true);
    }

    public override string PopUpName()
    {
        return "Loading";
    }
}
