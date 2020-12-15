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
        //Managers.UI.ShowSceneUI<UI_Inven>();

        gameObject.GetOrAddComponent<CursorController>();

       GameObject player = Managers.Game.Spawn(Define.WorldObject.Player, "UnityChan");
       Camera.main.gameObject.GetOrAddComponent<CameraController>().SetPlayer(player);
        
        Managers.Game.Spawn(Define.WorldObject.Monster, "Knight");

        Managers.Sound.Play("Sounds/GameSound/AboveTheTreetops", Define.Sound.Bgm,0.45f);

        GameObject go = new GameObject { name = "SpawningPool" };
        SpawningPool pool =  go.GetOrAddComponent<SpawningPool>();

        //임시
        for (int i = 0; i < 30; i++)
        {
            Managers.Inven.Add(1000, Define.InvenType.Consume);
            Managers.Inven.Add(1002, Define.InvenType.Consume);
        }


        pool.SetKeepMonsterCount(5);
    }

    public override void Clear()
    {

    }
}

