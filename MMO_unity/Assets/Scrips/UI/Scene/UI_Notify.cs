using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Notify : UI_Popup
{

    GameObject _player;
    public Data.Item _item;


    public Data.Item Item { get { return _item; } set { _item = value; } }

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
        Init();
        //Get<GameObject>((int)GameObjects.YesButton).GetComponent<Button>().onClick = ;
        //Get<GameObject>((int)GameObjects.NoButton).GetComponent<Button>().onClick = ;
    }

    public void OnYesClick()
    {
        if(Item != null)
            _player.GetComponent<Equipment>().Add(_item);

        _player.GetComponent<Inventory>().Remove(_item.itemTemplateId);

        base.ClosePopupUI();
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
