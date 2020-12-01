using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_SpeechBox : UI_Popup
{
    public int talkIndex;
    
    Text _nameBox;
    Text _speechBox;

    int _screenPlayIndex;
    enum GameObjects
    {
        TalkText,
        NameText,
    }

    // Start is called before the first frame update
    public void Awake()
    {
        Bind<GameObject>(typeof(GameObjects));
        _speechBox = Get<GameObject>((int)GameObjects.TalkText).GetComponent<Text>();
        _nameBox = Get<GameObject>((int)GameObjects.NameText).GetComponent<Text>();
        _screenPlayIndex = 0;

    }

    public bool TalkingAction(int NpcId)
    {
        int questTalkIndex = Managers.Quest.GetQuestTalkIndex(NpcId);
        if (Managers.Data.NpcDict[NpcId + questTalkIndex].screenPlay.Length == _screenPlayIndex) 
            return true;

        

        _nameBox.text = Managers.Data.NpcDict[NpcId + questTalkIndex].name;
        _speechBox.text = Managers.Data.NpcDict[NpcId + questTalkIndex].screenPlay[_screenPlayIndex++];
        return false;
    }
    public override string PopUpName()
    {
        return null;
    }

}
