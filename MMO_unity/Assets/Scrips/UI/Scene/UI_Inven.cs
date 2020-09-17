using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Inven : UI_Scene
{
    // Start is called before the first frame update
    enum GameObjects
    {
        GridPanel,
    
    }

    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(GameObjects));

        GameObject gridPanel = Get<GameObject>((int)GameObjects.GridPanel);

        //기존에 있던 아이템들을 삭제
        foreach (Transform child in gridPanel.transform)
            Managers.Resource.Destory(child.gameObject);

        //실제 아이템들에 정보를 참고해서
        for(int i = 0; i < 8; i++)
        {
            GameObject item = Managers.UI.MakeSubItem<UI_Inven_Item>(gridPanel.transform).gameObject;

            UI_Inven_Item InvenItem= item.GetOrAddComponent<UI_Inven_Item>();
            InvenItem.SetInfo($"집행검 {i}번");
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
