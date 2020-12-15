using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager
{
    public Action actAfterSelect;
    public bool _isTalking = false;

    public int _screenPlayIndex;
    public int _NpcID;

    UI_SpeechBox _speechBox;

    //F키를 눌렀을때 실행.
    public void SpeakWithNpc(int mNpcID = 0)
    {
        mNpcID += OnQuest(mNpcID);
        //첫 대화인것.
        if (_speechBox == null && mNpcID != 0)
        {
            _isTalking = true;
            _NpcID = mNpcID;
            _speechBox = Managers.UI.ShowPopupUI<UI_SpeechBox>("UI_Speech");
            _screenPlayIndex = 0;
        }

        //대화가 끝났다는 것.
        Debug.Log($"{Managers.Data.NpcDict[_NpcID].screenPlay.Length} == {_screenPlayIndex}");
        if (Managers.Data.NpcDict[_NpcID].screenPlay.Length == _screenPlayIndex)
        {
            //_NpcID, _screenPlayIndex
            FinishSpeech();
            return;
        }

        Debug.Log($"{_NpcID}, {_screenPlayIndex}");
        _speechBox.TalkingAction(_NpcID, _screenPlayIndex);
    }

    //진행중인 퀘스트 인지 
    public int OnQuest(int id)
    {
        //대화하는 NPC가 진행중인 퀘스트 인지 확인
        int goingQuest = id + 100;

        //진행중이지만 아직 못끝냈을 때
        if (Managers.Quest.QuestActive.IndexOf(goingQuest) != -1)
        {
            return (int)Define.Quest.Quest + (int)Define.Quest.OnGoing;
        }
        else if (Managers.Quest.ReachQuest.IndexOf(goingQuest) != -1)
        {
            return (int)Define.Quest.Quest + (int)Define.Quest.Reached;
        }
        else if (Managers.Quest.FinshQuest.IndexOf(goingQuest) != -1)
        {
            return (int)Define.Quest.Quest + (int)Define.Quest.Complete;
        }

        return 0;
    }

    public void ConversationByChoice(int mSelectionNum)
    {
        _NpcID += mSelectionNum;
    }

    public void StartNewSpeechAgain()
    {
        _screenPlayIndex = 0;
    }

    public void NextSpeech()
    {
        _screenPlayIndex += 1;
    }

    public void FinishSpeech()
    {
        Debug.Log("대화 종료@@@@@@@@@");
        Managers.UI.ClosePopupUI(_speechBox);
        _isTalking = false;
        _speechBox = null;
    }
}
