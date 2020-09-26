using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginScene : BaseScene
{
    // Start is called before the first frame update
    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Login;

        List<GameObject> list = new List<GameObject>();
      
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {

            //SceneManager.LoadSceneAsync(); 어씽크 : 비동기 동기 규모가 큰 RPG같은 경우 시간이 걸림. 즉 먼저 다음 화면의 정보들을 미리 백그라운데에서 실행하는 것. 즉 로딩
            //SceneManager.LoadScene("Game"); //LoadScene은 현재있는걸 한번에 날리고 다음껄 실행함
            Managers.Scene.LoadScene(Define.Scene.Game);
        }

    }

    public override void Clear()
    {
        Debug.Log("LoginScene Clear");
    }

}
