using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_DmgTxt : UI_Base
{
    float moveSpeed = 2.0f;

    enum GameObjects
    {
        DamageText,
    }
    public override void Init()
    {
        base.Bind<GameObject>(typeof(GameObjects));
    }

    void Start()
    {
        Destroy(gameObject, 1f);
    }

    void Update()
    {
        //로컬 좌표
        //transform.Translate(Vector3.up * Time.deltaTime * moveSpeed);
        transform.position += new Vector3(0, moveSpeed * Time.deltaTime, 0);
    }

    public void SetDmgText(int damage, bool isCritical, bool isEvasive, Color color)
    {
        if (!isEvasive)
        {
            if (isCritical)
            {

                gameObject.transform.GetChild(0).GetComponent<TextMesh>().color = Color.red;
                gameObject.transform.GetChild(0).GetComponent<TextMesh>().fontSize = 45;
                gameObject.transform.GetChild(0).GetComponent<TextMesh>().text = $"{damage}";
            }
            else
            {
                gameObject.transform.GetChild(0).GetComponent<TextMesh>().color = color;
                gameObject.transform.GetChild(0).GetComponent<TextMesh>().fontSize = 30;
                gameObject.transform.GetChild(0).GetComponent<TextMesh>().text = $"{damage}";
            }
        }
        else
        {
            gameObject.transform.GetChild(0).GetComponent<TextMesh>().color = new Vector4(0.37f,0f,1.0f,1);
            gameObject.transform.GetChild(0).GetComponent<TextMesh>().fontSize = 45;
            gameObject.transform.GetChild(0).GetComponent<TextMesh>().text = $"MISS";
        }
        
    }
}
