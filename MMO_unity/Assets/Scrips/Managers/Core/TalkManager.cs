using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager
{
    public Action<int, bool> NearNPCDownButtonF;

    public bool _isTalking = false;

    //F키를 눌렀을때 실행.
    public void TalkNPC(int npcId, bool isTalking)
    {
        if (NearNPCDownButtonF != null)
        {
            NearNPCDownButtonF(npcId, isTalking);
        }
    }

}
