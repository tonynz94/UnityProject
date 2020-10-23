using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Inven : UI_Popup
{
    //아이템 창 슬롯
    public List<Item> items = new List<Item>();

    enum GameObjects
    {
        GridPanel, 
    }

    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(GameObjects));

        GameObject gridPanel = Get<GameObject>((int)GameObjects.GridPanel);

    }

    public void Add(Item item)
    {
        if(item != null)
        {
            Debug.Log(item.name);
            items.Add(item);
        }
    }

    public void Remove(Item item)
    {
        if (item != null)
        {
            items.Remove(item);
        }
    }

}
