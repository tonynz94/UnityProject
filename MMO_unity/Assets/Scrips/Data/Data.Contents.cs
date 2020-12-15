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

    #region ConsumeItem
    [Serializable]
    public class ConsumeItem
    {
        public int consumeItemId;  //변수의 이름과 json의 이름이 같지 않으면 찾지 못함.
        public string name;      
        public int hp;
        public int mp;
        public string description;

        public Sprite icon;
    }
    //인터페이스를 상속받음
    [Serializable]
    public class ConsumeItemData : ILoader<int, ConsumeItem>
    {
        Sprite tempIcon;
        public List<ConsumeItem> consumeItems = new List<ConsumeItem>();

        public Dictionary<int, ConsumeItem> MakeDict()
        {
            Dictionary<int, ConsumeItem> dict = new Dictionary<int, ConsumeItem>();
            foreach (ConsumeItem consumeItem in consumeItems)
            {            
                dict.Add(consumeItem.consumeItemId, consumeItem);
                dict[consumeItem.consumeItemId].icon = Resources.Load<Sprite>($"Textures/ConsumeItem/{consumeItem.name} Icon");
            }
            return dict;
        }
    }
    #endregion

    #region OtherItem
    [Serializable]
    public class OtherItem
    {
        public int otherItemId;  //변수의 이름과 json의 이름이 같지 않으면 찾지 못함.
        public string name;
        public string description;

        public Sprite icon;
    }
    //인터페이스를 상속받음
    [Serializable]
    public class OtherItemData : ILoader<int, OtherItem>
    {
        Sprite tempIcon;
        public List<OtherItem> otherItems = new List<OtherItem>();

        public Dictionary<int, OtherItem> MakeDict()
        {
            Dictionary<int, OtherItem> dict = new Dictionary<int, OtherItem>();
            foreach (OtherItem otherItem in otherItems)
            {
                dict.Add(otherItem.otherItemId, otherItem);
                dict[otherItem.otherItemId].icon = Resources.Load<Sprite>($"Textures/OtherItem/{otherItem.name} Icon");
            }
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
        public string description;
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
            speech[2] = new SpeechSelection("이 섬도 안전하지 않으니 무기를 만드는것이 좋을꺼야", false);
            speech[3] = new SpeechSelection("안쪽에 들어가봐, 무기를 제작해주는 사람이 있을꺼야.", false);
            npcs.Add(new NPC(1000, "죠지", speech));
            speech = null;

            speech = new SpeechSelection[3];
            speech[0] = new SpeechSelection("너가 크로토피 친구구만", false);
            speech[1] = new SpeechSelection("이 섬은 비밀이라는게 없네 친구", false);
            speech[2] = new SpeechSelection("무기가 없는거 같은데, 재료만 가져다주면 무기를 만들어주겠네, 어떤가?", true);
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

            //퀘스트 진행 중 완료 했을때
            speech = new SpeechSelection[1];
            speech[0] = new SpeechSelection("오 재료를 다 모아온 모양이군 무기는 여기있네 행운울 비네. ", true);
            npcs.Add(new NPC(2110, "제인", speech));
            speech = null;
            
            //퀘스트 진행 중 완료 못했을 때
            speech = new SpeechSelection[1];
            speech[0] = new SpeechSelection("아직 재료를 못구한 모양이구만 조금만 힘내보라고~", false);
            npcs.Add(new NPC(2120, "제인", speech));
            speech = null;

            speech = new SpeechSelection[1];
            speech[0] = new SpeechSelection("내가 준 무기는 마음에 드는가??", false);
            npcs.Add(new NPC(2130, "제인", speech));
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


            speech = new SpeechSelection[3];
            speech[0] = new SpeechSelection("안녕~ 처음보는 친구구만", false);
            speech[1] = new SpeechSelection("우리가 요즘 먹을게 없어서 식탁이 많이 비었있단 말이지..", false);
            speech[2] = new SpeechSelection("혹시 내 부탁좀 들어줄 수 있는가??..", true);
            npcs.Add(new NPC(4000, "요리사 제드", speech));
            speech = null;


            speech = new SpeechSelection[2];
            speech[0] = new SpeechSelection("좋은 생각이야 친구 당신에게 맞는 보상을 꼭 해주지", false);
            speech[1] = new SpeechSelection("저기 물가에서 물고기 3마리좀 잡아줘 ", false);
            npcs.Add(new NPC(4100, "요리사 제드", speech));
            speech = null;

            speech = new SpeechSelection[2];
            speech[0] = new SpeechSelection("오 정말 고맙네 친구!! 오늘 우리 마을이 배부르게 먹을 수 있겠어", false);
            speech[1] = new SpeechSelection("자 보상은 모험가에게 꼭 필요한 갑옷이네!! 당신을 지켜줄꺼야 ", true);
            npcs.Add(new NPC(4110, "요리사 제드", speech));
            speech = null;

            speech = new SpeechSelection[1];
            speech[0] = new SpeechSelection("아직 생선을 다 못잡은거 같은데..", false);
            npcs.Add(new NPC(4120, "요리사 제드", speech));
            speech = null;

            speech = new SpeechSelection[1];
            speech[0] = new SpeechSelection("내가 준 갑옷은 마음에 드는가??", false);
            npcs.Add(new NPC(4130, "요리사 제드", speech));
            speech = null;

            speech = new SpeechSelection[1];
            speech[0] = new SpeechSelection("싫다면 어쩔 수 없고.. 다른 사람한테 부탁할 수 밖에", false);
            npcs.Add(new NPC(4200, "요리사 제드", speech));
            speech = null;



            speech = new SpeechSelection[3];
            speech[0] = new SpeechSelection("오 안녕 친구", false);
            speech[1] = new SpeechSelection("해가 지면 곧 추워질텐데 불을 때워야 할 장작이 부족해..", false);
            speech[2] = new SpeechSelection("혹시 장작좀 가져다 줄 수 있나 친구??", true);
            npcs.Add(new NPC(5000, "케인", speech));
            speech = null;

            speech = new SpeechSelection[2];
            speech[0] = new SpeechSelection("고맙네 친구!!", false);
            speech[1] = new SpeechSelection("보상은 배를 채울 수 있는 먹을 것을 주겠네 그럼 부탁하네~", false);
            npcs.Add(new NPC(5100, "케인", speech));
            speech = null;

            speech = new SpeechSelection[2];
            speech[0] = new SpeechSelection("오 벌써 구해 온거야??", false);
            speech[1] = new SpeechSelection("이거면 며칠은 따뜻하게 주민들이 잘 수 추운 날을 보낼 수 있겠어~ 고맙네", true);
            npcs.Add(new NPC(5110, "케인", speech));
            speech = null;

            speech = new SpeechSelection[1];
            speech[0] = new SpeechSelection("장작이 부족한거 같은데...", false);
            npcs.Add(new NPC(5120, "케인", speech));
            speech = null;

            speech = new SpeechSelection[1];
            speech[0] = new SpeechSelection("덕분에 따뜻한 밤을 보냈어 친구~", false);
            npcs.Add(new NPC(5130, "케인", speech));
            speech = null;

            speech = new SpeechSelection[1];
            speech[0] = new SpeechSelection("후회할텐데.. 아주 맛있는 음식으로 보상 할려고했는데..", false);
            npcs.Add(new NPC(5200, "케인", speech));
            speech = null;





            speech = new SpeechSelection[2];
            speech[0] = new SpeechSelection("우리 마을이 습격당할려고 해..", false);
            speech[1] = new SpeechSelection("근처에 있는 Knight 몬스터좀 20마리 죽여 줄 수 있어??", true);
            npcs.Add(new NPC(6000, "토니", speech));
            speech = null;

            speech = new SpeechSelection[2];
            speech[0] = new SpeechSelection("아 진짜?? 꼭 아이템을 장착하고 싸워야 해.", false);
            speech[1] = new SpeechSelection("만약 아이템이 없다면 근처에 있는 분들에게 말을 걸어봐바", false);
            npcs.Add(new NPC(6100, "토니", speech));
            speech = null;

            speech = new SpeechSelection[2];
            speech[0] = new SpeechSelection("우와.. 20마리 다 잡은거야??", false);
            speech[1] = new SpeechSelection("보기와 다르게 전투 능력이 대단한걸?? 고마워", true);
            npcs.Add(new NPC(6110, "토니", speech));
            speech = null;

            speech = new SpeechSelection[1];
            speech[0] = new SpeechSelection("조심해...", false);
            npcs.Add(new NPC(6120, "토니", speech));
            speech = null;

            speech = new SpeechSelection[1];
            speech[0] = new SpeechSelection("우리의 영웅 덕분에 마을 분위기가 좋아졌어~", false);
            npcs.Add(new NPC(6130, "토니", speech));
            speech = null;

            speech = new SpeechSelection[1];
            speech[0] = new SpeechSelection("후회할텐데.. 아주 맛있는 음식으로 보상 할려고했는데..", false);
            npcs.Add(new NPC(6200, "토니", speech));
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
            acts.Add(new Act(2100, () => { Managers.Quest.UpdateQuestAdd(2100);} ));
            acts.Add(new Act(2110, () => { Managers.Quest.FinishQuest(2100); } ));

            acts.Add(new Act(3100, () => { Managers.Game.BossDoorOpenClose(Define.BossDoor.Open); } ));
            
            acts.Add(new Act(4100, () => { Managers.Quest.UpdateQuestAdd(4100); }));
            acts.Add(new Act(4110, () => { Managers.Quest.FinishQuest(4100); }));

            acts.Add(new Act(5100, () => { Managers.Quest.UpdateQuestAdd(5100); }));
            acts.Add(new Act(5110, () => { Managers.Quest.FinishQuest(5100); }));

            acts.Add(new Act(6100, () => { Managers.Quest.UpdateQuestAdd(6100); }));
            acts.Add(new Act(6110, () => { Managers.Quest.FinishQuest(6100); }));
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
            quests.Add(new Quest(2100, "제인의 부탁","돌조각 3개 채집하기.", 0 , 0, new int[] { 10001, 20001},
                            new QuestGoal(Define.QuestGoalType.Gathering, 
                            new Dictionary<int, int> { { 101, 3 } } ,   //Require
                            new Dictionary<int, int> { { 101, 0 } } ))); //current << 퀘스트를 받을때 인벤토리르 검사하여 current 값 높여주기!!

            quests.Add(new Quest(4100, "제드의 부탁", "낚시하여 생선 3마리 잡아오기.", 0, 0, new int[] { 30001, 40001, 60001 },
                            new QuestGoal(Define.QuestGoalType.Gathering,
                            new Dictionary<int, int> { { 102, 3 } },   //Require
                            new Dictionary<int, int> { { 102, 0 } }))); //current << 퀘스트를 받을때 인벤토리르 검사하여 current 값 높여주기!!

            quests.Add(new Quest(5100, "케인의 부탁", "나무 토막 5개 채집하기.", 0, 0, new int[] {1004},
                            new QuestGoal(Define.QuestGoalType.Gathering,
                            new Dictionary<int, int> { { 100, 3 } },   //Require
                            new Dictionary<int, int> { { 100, 0 } }))); //current << 퀘스트를 받을때 인벤토리르 검사하여 current 값 높여주기!!

            quests.Add(new Quest(6100, "토니의 부탁", "Knight 20마리 잡기.", 0, 0, new int[] { 10001 },
                            new QuestGoal(Define.QuestGoalType.Gathering,
                            new Dictionary<int, int> { { 100, 3 } },   //Require
                            new Dictionary<int, int> { { 100, 0 } })));
        }
    }

    #endregion
}




