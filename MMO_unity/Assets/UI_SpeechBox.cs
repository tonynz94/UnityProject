using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_SpeechBox : UI_Base
{
    public int talkIndex;
    enum GameObjects
    {
        TalkText,
        NameText,
    }

    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));
        
        Managers.Talk.NearNPCDownButtonF -= TalkingAction;
        Managers.Talk.NearNPCDownButtonF += TalkingAction;
        gameObject.SetActive(false); 
    }

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

    }

    // Update is called once per frame
    public void TalkingAction(GameObject scanObj, bool isTalking)
    {
        //대화가 시작되면 스피치박스를 켜 줌.
        if (isTalking)
        {
            gameObject.SetActive(true);
            Text speechBox = Get<GameObject>((int)GameObjects.TalkText).GetComponent<Text>();
            Text nameBox = Get<GameObject>((int)GameObjects.NameText).GetComponent<Text>();

            NPCController npc = scanObj.GetComponent<NPCController>();

            speechBox.text = npc.Speech;
            nameBox.text = npc.Name;
        }
        else
        {
            gameObject.SetActive(false);
        }
    }


    public void TalkingAction(int id, bool isTalking)
    {
        string talkData = Managers.Talk.GetTalk(id, talkIndex);

        Text speechBox = Get<GameObject>((int)GameObjects.TalkText).GetComponent<Text>();
        Text nameBox = Get<GameObject>((int)GameObjects.NameText).GetComponent<Text>();

        speechBox.text = talkData;
        nameBox.text = talkData;
    }


}
