using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    #region Stat
    [Serializable]
    public class Stat
    {
        public int level;  //변수의 이름과 json의 이름이 같지 않으면 찾지 못함.
        public int maxHp;
        public int attack;
        public int totalExp;
    }

    //인터페이스를 상속받음
    [Serializable]
    public class StatData : ILoader<int, Stat>
    {
        public List<Stat> stats = new List<Stat>();

        public Dictionary<int, Stat> MakeDict()
        {
            Dictionary<int, Stat> dict = new Dictionary<int, Stat>();
            foreach (Stat stat in stats)
                dict.Add(stat.level, stat);
            return dict;
        }
    }
    #endregion
    
    #region Item
    [Serializable]
    public class Item
    {
        public int itemTemplateId;  //변수의 이름과 json의 이름이 같지 않으면 찾지 못함.
        public string name;
        public int attack;
        public int defense;
        public int critical;
        public Sprite icon;
    }
    //인터페이스를 상속받음
    [Serializable]
    public class ItemData : ILoader<int, Item>
    {
        Sprite tempIcon;
        public List<Item> items = new List<Item>();

        public Dictionary<int, Item> MakeDict()
        {
            Dictionary<int, Item> dict = new Dictionary<int, Item>();
            foreach (Item item in items)
            {
                dict.Add(item.itemTemplateId, item);            
                dict[item.itemTemplateId].icon = Resources.Load<Sprite>($"Textures/Item/{item.name}");
            }
            return dict;
        }
    }
    #endregion
}



