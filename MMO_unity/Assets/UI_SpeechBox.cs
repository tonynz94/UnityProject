using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_SpeechBox : UI_Popup
{
    int _talkingNPCID;
    
    Text _nameBox;
    Text _speechBox;
    Button _yesButton;
    Button _noButton;

    enum GameObjects
    {
        TalkText,
        NameText,
        YesButton,
        NoButton,
    }

    // Start is called before the first frame update
    public void Awake()
    {
        Bind<GameObject>(typeof(GameObjects));
        _speechBox = Get<GameObject>((int)GameObjects.TalkText).GetComponent<Text>();
        _nameBox = Get<GameObject>((int)GameObjects.NameText).GetComponent<Text>();
        _yesButton = Get<GameObject>((int)GameObjects.YesButton).GetComponent<Button>();
        _noButton = Get<GameObject>((int)GameObjects.NoButton).GetComponent<Button>();
        _yesButton.gameObject.SetActive(false);
        _noButton.gameObject.SetActive(false);
    }

    public void TalkingAction(int mNpcID, int mScreenPlayIndex)
    {
        _talkingNPCID = mNpcID;
        Debug.Log($"_talkingNPCID : {_talkingNPCID}");
        //int questTalkIndex = Managers.Quest.GetQuestTalkIndex(NpcId);
        _nameBox.text = Managers.Data.NpcDict[_talkingNPCID].name;
        _speechBox.text = Managers.Data.NpcDict[_talkingNPCID].screenPlay[mScreenPlayIndex].speech;
        bool select = Managers.Data.NpcDict[_talkingNPCID].screenPlay[mScreenPlayIndex].isSelection;
        Debug.Log($"select : {select}");
        if (select)
        {
            Debug.Log("선택지가 있는 말유아이");
            _yesButton.gameObject.SetActive(true);
            _noButton.gameObject.SetActive(true);
            return;
        }
        else
        {
            _yesButton.gameObject.SetActive(false);
            _noButton.gameObject.SetActive(false);
        }

        Managers.Talk.NextSpeech();
    }
    public override string PopUpName()
    {
        return null;
    }

    
    public void YesButtonClick()
    {
        //퀘스트를 받는것 일 수도 있으며 다른 동작을 하는 것일 수 도 있음.
        Debug.Log("Yes Click");
        Managers.Talk.StartNewSpeechAgain();

        //지금 이 확인 버튼을 누르면 100으로 가는 것 때문에 오류가 나고 있음.
        Managers.Talk.ConversationByChoice(100);
        
        //여기서 오류가 나는것 ~~~~~~~~~~@@@@@@@@@@@@@@@@@@@@@
        Managers.Talk.SpeakWithNpc();

        //수행.
        Managers.Talk.actAfterSelect -= Managers.Data.ActDict[_talkingNPCID].act;
        Managers.Talk.actAfterSelect += Managers.Data.ActDict[_talkingNPCID].act;
        if (Managers.Talk.actAfterSelect != null)
        {
            Managers.Talk.actAfterSelect.Invoke();
        }
    }

    public void NoButtonClick()
    {
        Debug.Log("No Click");
        Managers.Talk.StartNewSpeechAgain();
        Managers.Talk.ConversationByChoice(200);
        Managers.Talk.SpeakWithNpc();

        //상황에 따라 문일 열리고 또는 퀘스트 목록에 추가.
    }
}
