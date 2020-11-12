using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_TalkBox : UI_Base
{

    public GameObject scanObject;

    enum GameObjects
    {
        TalkText,
    }

    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));
    }

    public void Action(GameObject scanObj)
    {
        scanObject = scanObj;
    }
}
