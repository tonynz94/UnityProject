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
        
    }

    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(GameObjects));

        GameObject gridPanel = Get<GameObject>((int)GameObjects.GridPanel);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
