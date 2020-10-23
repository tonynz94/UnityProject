using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Scene : UI_Base
{
    // Start is called before the first frame update
    public override void Init()
    {
        //sorting이 필요 없음. false(order에 들어가지 않음)
        Managers.UI.SetCanvas(gameObject, false);
    }
}
