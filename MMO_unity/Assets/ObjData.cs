using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjData : MonoBehaviour
{
//NPC에 있는 것들.
    public Define.InteractionType _InteractionType;
    public Animator _anim;
    public string guideText;
    public int _Id;

    public void Awake()
    {
        _anim = GetComponent<Animator>();
    }


    public void Talking(bool talking)
    {
        if(_InteractionType == Define.InteractionType.NPC)
            _anim.SetBool("Talking", talking);
    }

}
