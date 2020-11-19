using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager
{
    public int _questId;
    public int _questActionIndex = 0; //퀘스트 순서

    public int GetQuestTalkIndex(int id)
    {
        return _questId + _questActionIndex;
    }

    //대화가 끝이 났을때. 올려줌.(대화를 나눴던 npc id)
    public string CheckQuest(int id)
    {
        //
        if(id == Managers.Data.QuestDict[_questId].npcId[_questActionIndex])
            _questActionIndex++;

        if (_questActionIndex == Managers.Data.QuestDict[_questId].npcId.Length)
            nextQuest();

        return Managers.Data.QuestDict[_questId].questName;
    }

    //새로운 퀘스트가 들어왔을 시.
    void nextQuest()
    {
        _questId += 10;
        _questActionIndex = 0;
    }
}
