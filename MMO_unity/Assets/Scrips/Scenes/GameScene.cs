using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{

    // Start is called before the first frame update

    Coroutine co;

    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Game;

        //인벤토리 열기
        //Managers.UI.ShowSceneUI<UI_Inven>();

        gameObject.GetOrAddComponent<CursorController>();
        
        GameObject player = Managers.Game.Spawn(Define.WorldObject.Player, "UnityChan");
        Camera.main.gameObject.GetOrAddComponent<CameraController>().SetPlayer(player);

        Managers.Game.Spawn(Define.WorldObject.Monster, "Knight");

        GameObject go = new GameObject { name = "SpawningPool" };
        SpawningPool pool =  go.GetOrAddComponent<SpawningPool>();
        pool.SetKeepMonsterCount(5);
    }

    public override void Clear()
    {

    }
}

