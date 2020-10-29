using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Notify : UI_Popup
{

    GameObject _player;

    enum GameObjects
    {
        TitleText,
        YesButton,
        NoButton,
    }

    private void Awake()
    {
        _player = Managers.Game.GetPlayer();
        Bind<GameObject>(typeof(GameObjects));
    }

    // Start is called before the first frame update
    void Start()
    {
        Get<GameObject>((int)GameObjects.YesButton).GetComponent<Button>().onClick = ;
        Get<GameObject>((int)GameObjects.NoButton).GetComponent<Button>().onClick = ;
    }

    public void OnYesClick()
    {
        //클릭한 아이템의 정보를 받아와야함.
        _player.GetComponent<Equipment>().wearItems[]
        //캐릭터 인벤토리 리스트에 추가하기.
    }

    public void OnNoClick()
    {
        //자기 자신 삭제
        base.ClosePopupUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
