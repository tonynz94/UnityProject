﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UI_Popup : UI_Base
{
    // Start is called before the first frame update

    //외부에서 드래그앤드롭으로 UI을 열었을 시 체크하기 위함
    public abstract string PopUpName();
    public override void Init()
    {
        Managers.UI.SetCanvas(gameObject, true);
    }

    public virtual bool ClosePopupUI()
    {
        return Managers.UI.ClosePopupUI(this);
    }


}
