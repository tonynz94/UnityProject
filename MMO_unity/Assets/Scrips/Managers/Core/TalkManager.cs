using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager
{
    public Action<GameObject, bool> NearNPCDownButtonF;
    Dictionary<int, string[]> talkData = new Dictionary<int, string[]>();
    public bool _isTalking = false;

    //F키를 눌렀을때 실행.
    public void TalkNPC(GameObject talkNPC, bool isTalking)
    {
        if (NearNPCDownButtonF != null)
        {
            NearNPCDownButtonF(talkNPC, isTalking);
        }
    }

    void GenerateData()
    {
        talkData.Add(1000, new string[] { "뭐야 처음보는 친군데??", "이 섬에 어떻게 들어온거야??"});
        talkData.Add(2000, new string[] { "남편이 왜 안들어오지..", "곧 있으면 어두워질텐데.." });
        talkData.Add(3000, new string[] { "너랑 말할 기분이 아니야. 저리가" }); 
    }

    public string GetTalk(int id, int talkIndex)
    {
        return talkData[id][talkIndex];
    }
}
