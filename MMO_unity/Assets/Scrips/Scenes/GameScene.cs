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
        Managers.UI.ShowSceneUI<UI_Inven>();

        gameObject.GetOrAddComponent<CursorController>();

        //co = StartCoroutine("ExplodeAfterSeconds", 4.0f);
        //StartCoroutine("CoStopExplode", 2.0f);
    }

    /*IEnumerator CoStopExplode(float seconds)
    {
        if(co != null)
        {
            yield return new WaitForSeconds(seconds);
            StopCoroutine(co);
            Debug.Log("Explode Stop");
        }
    }

    IEnumerator ExplodeAfterSeconds(float seconds)
    {
        Debug.Log("Explode Enter");
        yield return new WaitForSeconds(seconds);
        Debug.Log("Explode Boooooom");
        co = null;
    }*/
    public override void Clear()
    {

    }
}

