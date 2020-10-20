using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DmgTxt : UI_Base
{
    [SerializeField]
    float moveSpeed;

    Stat _stat;

    GameObject _player;

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
        _stat = transform.parent.GetComponent<Stat>();
    }

    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * moveSpeed);
    }
}
