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

        }

    }

    public override void Clear()
    {
        Debug.Log("LoginScene Clear");
    }
}
