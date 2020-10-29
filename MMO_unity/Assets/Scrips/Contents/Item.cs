using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    [SerializeField]
    protected int _itemTemplateId;
    [SerializeField]
    protected string _name;
    [SerializeField]
    protected int _attack;
    [SerializeField]
    protected int _defense;
    [SerializeField]
    protected int _critical;
    [SerializeField]
    protected Sprite _icon;


    public int ItemTemplateId { get { return _itemTemplateId; } set { _itemTemplateId = value; } }
    public string Name { get { return _name; } set { _name = value; } }
    public int Attack { get { return _attack; } set { _attack = value; } }
    public int Defense { get { return _defense; } set { _defense = value; } }
    public int Critical { get { return _critical; } set { _critical = value; } }

    public Sprite Icon { get { return _icon; } set { Icon = value; } }

    protected virtual void Start()
    {

    }

    public void SetItem(int itemTemplateId)
    {
        Dictionary<int, Data.Item> dict = Managers.Data.ItemDict;

        Data.Item item = dict[itemTemplateId];
        _name = item.name;
        _attack = item.attack;
        _defense = item.defense;
        _critical = item.critical;
    }
}
