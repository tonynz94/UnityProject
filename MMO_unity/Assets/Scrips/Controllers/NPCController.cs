using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : BaseController
{
    [SerializeField]
    float radius = 1.0f;

    [SerializeField]
    string _speech;
    [SerializeField]
    string _name;
    [SerializeField]
    Sprite _image;

    GameObject _player;

    public string Speech { get { return _speech; } }
    public string Name { get { return _name; } }
    public Sprite image { get { return _image; } }

    public override void Init()
    {
        _player = Managers.Game.GetPlayer();
    }

}
