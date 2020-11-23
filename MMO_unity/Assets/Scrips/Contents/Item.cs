using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    [SerializeField]
    private int _itemTemplateId;

    public int ItemTemplateId { get; private set; }
}
