using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : BaseController
{
    [SerializeField]
    float radius = 1.0f;

    GameObject _player;

    public override void Init()
    {
        _player = Managers.Game.GetPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
