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

    public class SpeechSelection
    {
        public string speech;
        public bool isSelection;

        public SpeechSelection(string mSpeech, bool mIsSecection)
        {
            speech = mSpeech;
            isSelection = mIsSecection;
        }
    }
    public class NPC
    {
        public int npcId;
        public string name;
        public SpeechSelection[] screenPlay;
        
        public NPC(int mId, string mName, SpeechSelection[] mScreenPlay)
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
            SpeechSelection[] speech;
            
            speech = new SpeechSelection[4];
            speech[0] = new SpeechSelection("처음보는 친군데?", false);
            speech[1] = new SpeechSelection("복장을 보아하니 현재 전쟁중인 크로토피 사람이구만", false);
            speech[2] = new SpeechSelection("여튼 이 섬도 안전하지 않으니 무기를 만드는것이 좋을꺼야", false);
            speech[3] = new SpeechSelection("안쪽에 들어가봐, 무기를 제작해주는 사람이 있을꺼야.", false);
            npcs.Add(new NPC(1000, "죠지", speech));
            speech = null;

            speech = new SpeechSelection[3];
            speech[0] = new SpeechSelection("너가 크로토피 친구구만", false);
            speech[1] = new SpeechSelection("이 섬은 비밀이라는게 없네 친구", false);
            speech[2] = new SpeechSelection("무기가 없는거 같은데, 재료만 가져다주면 무기를 만들어주겠네, 어때 하겠는가?", true);
            npcs.Add(new NPC(2000, "제인", speech));
            speech = null;

            //Yes Click
            speech = new SpeechSelection[2];
            speech[0] = new SpeechSelection("좋은 생각이야 친구", false);
            speech[1] = new SpeechSelection("돌조각 3개와, 나무조각 3개만 가져와 주겠나?? ", false);
            npcs.Add(new NPC(2100, "제인", speech));
            speech = null;

            //No Click
            speech = new SpeechSelection[1];
            speech[0] = new SpeechSelection("무기 만들고 싶을때 언제든지 이야기 하라고~", false);
            npcs.Add(new NPC(2200, "제인", speech));
            speech = null;

            speech = new SpeechSelection[2];
            speech[0] = new SpeechSelection("여기는 정말 위험한 곳이야..", false);
            speech[1] = new SpeechSelection("그래도 들어가고 싶은가?", true);
            npcs.Add(new NPC(3000, "문지기", speech));
            speech = null;

            //Yes 선택 시
            speech = new SpeechSelection[1];
            speech[0] = new SpeechSelection("행운을 비네, 이 마을의 평화를 꼭 가져와 주게나.", false);
            npcs.Add(new NPC(3100, "문지기", speech));
            speech = null;

            //No 선택 시
            speech = new SpeechSelection[1];
            speech[0] = new SpeechSelection("들어가고 싶으면 언제든지 이야기 하게나.", false);
            npcs.Add(new NPC(3200, "문지기", speech));
            speech = null;
        }
    }
    #endregion

    #region Act
    public class Act
    {
        public int npcId;
        public Action act;

        public Act(int mNpcId, Action mAct)
        {
            npcId = mNpcId;
            act = mAct;
        }
    }
    public class ActData
    {
        public static List<Act> acts = new List<Act>();

        public static Dictionary<int, Act> MakeDict()
        {
            GenerateData();
            Dictionary<int, Act> dict = new Dictionary<int, Act>();

            foreach (Act act in acts)
            {
                dict.Add(act.npcId, act);
            }

            return dict;
        }

        private static void GenerateData()
        {
            acts.Add(new Act(2100, () => { Managers.Quest.QuestAdd(2100);}));
            acts.Add(new Act(3100, () => { Managers.Game.BossDoorOpenClose(Define.BossDoor.Open); } ));
        }
    }
    #endregion

    #region Quest
    public class Quest
    {
        public int giverNpcID;
        public string title;    //제목
        public string description; //설명
        public int experienceReward;
        public int goldReward;
        public int[] itemReward;

        public QuestGoal questGoal;

        public Quest(int mGiverNpcID, string mTitle, string mDescription, int mExperienceReward, int mGoldReward, int[] mItemReward, QuestGoal mQuestGoal)
        {
            giverNpcID = mGiverNpcID;
            title = mTitle;
            description = mDescription;
            experienceReward = mExperienceReward;
            goldReward = mGoldReward;
            itemReward = mItemReward;
            questGoal = mQuestGoal;
        }
    }

    public class QuestGoal
    {
        public Define.QuestGoalType goalType;
        public Dictionary<int, int> requiredAmount;
        public Dictionary<int, int> currentAmount;

        public QuestGoal(Define.QuestGoalType mGoalType, Dictionary<int, int> mRequiredAmount, Dictionary<int, int> mCurrentAmount)
        {
            goalType = mGoalType;
            requiredAmount = mRequiredAmount;
            currentAmount = mCurrentAmount;
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
                dict.Add(quest.giverNpcID, quest);

            return dict;
        }

        private static void GenerateData()
        {
            quests.Add(new Quest(2100, "제인의 부탁","돌조각과 나무조각을 각각 3개씩 채집하세요.", 0 , 0, new int[] { 10001, 20001},
                            new QuestGoal(Define.QuestGoalType.Gathering, 
                            new Dictionary<int, int> { { 101, 3 }, { 102, 3 } } ,   //Require
                            new Dictionary<int, int> { { 101, 0 }, { 102, 0 } }))); //current << 퀘스트를 받을때 인벤토리르 검사하여 current 값 높여주기!!

        }
    }

    #endregion
}




