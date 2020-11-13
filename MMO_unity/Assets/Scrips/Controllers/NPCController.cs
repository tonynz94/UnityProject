using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : BaseController
{
    [SerializeField]
    float radius = 1.0f;

    [SerializeField]
    string _speech;

    GameObject _player;


    public string Speech { get { return _speech} };


    public override void Init()
    {
        _player = Managers.Game.GetPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
