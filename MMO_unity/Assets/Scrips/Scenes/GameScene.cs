using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{

    // Start is called before the first frame update


    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Game;

        //인벤토리 열기
        Managers.UI.ShowSceneUI<UI_Inven>();
    }
    public override void Clear()
    {

    }
}

