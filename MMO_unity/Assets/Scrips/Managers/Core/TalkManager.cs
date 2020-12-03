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
        //첫 대화인것.
        if (_speechBox == null && mNpcID != 0)
        {
            Debug.Log($"{mNpcID}");
            _isTalking = true;
            _NpcID = mNpcID;
            _speechBox = Managers.UI.ShowPopupUI<UI_SpeechBox>("UI_Speech");
            _screenPlayIndex = 0;
        }
        Debug.Log($"{mNpcID}");
        //대화가 끝났다는 것.
        if (Managers.Data.NpcDict[_NpcID].screenPlay.Length == _screenPlayIndex)
        {
            //_NpcID, _screenPlayIndex
            FinishSpeech();
            return;
        }

        _speechBox.TalkingAction(_NpcID, _screenPlayIndex);
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
        Managers.UI.ClosePopupUI(_speechBox);
        _isTalking = false;
        _speechBox = null;
    }
}
