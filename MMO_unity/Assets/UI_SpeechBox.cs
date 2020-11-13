using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_SpeechBox : UI_Base
{

    enum GameObjects
    {
        TalkText,
    }

    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));
    }

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

    }

    // Update is called once per frame
    public void TalkingAction(GameObject scanObj)
    {
        Text speechBox = Get<GameObject>((int)GameObjects.TalkText).GetComponent<Text>();
        NPCController npc = scanObj.GetComponent<NPCController>();
        speechBox.text = npc.Speech;
    }
        
}
