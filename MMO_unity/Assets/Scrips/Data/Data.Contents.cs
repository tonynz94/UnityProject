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
        public int maxMp;
        public int attack;
        public int defense;
        public int critical;
        public int evasive;
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
        public string equipPart;
        public int equipSlot;
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
                dict[item.itemTemplateId].icon = Resources.Load<Sprite>($"Textures/Item/{item.name} Icon");
                Debug.Log(dict[item.itemTemplateId].icon);
            }
            return dict;
        }
    }
    #endregion

    #region Skill
    [Serializable]
    public class Skill
    {
        public int skillTempelateId;
        public string skillName;  //변수의 이름과 json의 이름이 같지 않으면 찾지 못함.
        public float coolDown;
        public float skillDamage;
        public int requireLevel;
    }
    //인터페이스를 상속받음
    [Serializable]
    public class SkillData : ILoader<int, Skill>
    {
        public List<Skill> skills = new List<Skill>();

        public Dictionary<int, Skill> MakeDict()
        {
            Dictionary<int, Skill> dict = new Dictionary<int, Skill>();
            foreach (Skill skill in skills)
            {
                dict.Add(skill.skillTempelateId, skill);
            }
            return dict;
        }
    }


    #endregion

    #region NPC
    public class NPC
    {
        public int npcId;
        public string name;
        public string[] screenPlay;
        
        public NPC(int mId, string mName, string[] mScreenPlay)
        {
            npcId = mId;
            name = mName;
            screenPlay = mScreenPlay;
        }
    }

    //스태틱.
    public static class NPCData
    {
        public static List<NPC> npcs = new List<NPC>();

        public static Dictionary<int, NPC> MakeDict()
        {
            GenerateData();
            Dictionary<int, NPC> dict = new Dictionary<int, NPC>();

            foreach (NPC npc in npcs)
            {
                dict.Add(npc.npcId, npc);
            }

            return dict;
        }

        private static void GenerateData()
        {
            npcs.Add(new NPC(1000, "죠지", new String[] { "처음보는 친군데??", "이 섬에 언제 들어온거야??" }) );
            npcs.Add(new NPC(2000, "제인", new String[] { "벌써 어두워지고 있는데.."}));

            npcs.Add(new NPC(10 + 1000, "죠지", new String[] { "이 전쟁에 생존자가 있었구만..어떻게 이런일이..",
                                                                              "마을 안쪽에 제인 아줌마를 한번 찾아가볼래??",
                                                                              "아마 너에게 도움을 주실꺼야." }));

            npcs.Add(new NPC(11 + 2000, "제인", new String[] { "뭐야 생존자라니..",
                                                                              "일단 여기도 위험하니 무기를 만들어야 하는데..",
                                                                              "재료좀 구해와줄래?? 무기를 만들어줄게." }));

        }
    }

    #endregion

    #region Quest
    public class Quest
    {
        public int questId; //현재 진행중인 퀘스트
        public string questName;    //퀘스트 이름.
        public int[] npcId; //퀘스트와 연관된 다른 NPC 

        public Quest(int mQuestId, string mQuestName, int[] mNpcId)
        {
            questId = mQuestId;
            questName = mQuestName;
            npcId = mNpcId;
        }

    }

    public static class QuestData
    {
        public static List<Quest> quests = new List<Quest>();

        public static Dictionary<int, Quest> MakeDict()
        {
            GenerateData();
            Dictionary<int, Quest> dict = new Dictionary<int, Quest>();

            foreach (Quest quest in quests)
                dict.Add(quest.questId, quest);

            return dict;
        }

        private static void GenerateData()
        {
            quests.Add(new Quest(10, "첫 마을 방문", new int[] { 1000, 2000 }));
            quests.Add(new Quest(20, "무기 재료 구해오기", new int[] { 1000, 2000 }));

        }
    }

    #endregion
}




