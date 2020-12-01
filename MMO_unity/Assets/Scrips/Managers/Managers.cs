using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers s_Instance; //매니저는 static

    GameManager _game = new GameManager();
    InputManager _input = new InputManager();
    ResourceManager _resource = new ResourceManager();
    UIManager _ui = new UIManager();
    SceneManagerEx _scene = new SceneManagerEx();
    SoundManager _sound = new SoundManager();
    PoolManager _pool = new PoolManager();
    DataManager _data = new DataManager();
    TalkManager _talk = new TalkManager();
    QuestManager _quest = new QuestManager();
    InvenManager _invent = new InvenManager();
    EquipManager _equip = new EquipManager();
    SkillManager _skill = new SkillManager();


    public static Managers Instance { get { init(); return s_Instance; } }
    public static GameManager Game { get {  return Instance._game; } }
    public static InputManager Input { get { return Instance._input; } }
    public static ResourceManager Resource { get { return Instance._resource; } }
    public static UIManager UI { get { return Instance._ui; } }
    public static SceneManagerEx Scene { get { return Instance._scene; } }
    public static SoundManager Sound { get { return Instance._sound; } }
    public static PoolManager Pool { get { return Instance._pool; } }
    public static DataManager Data { get { return Instance._data; } }
    public static TalkManager Talk { get { return Instance._talk; } }
    public static QuestManager Quest { get { return Instance._quest; } }
    public static InvenManager Inven { get { return Instance._invent; } }
    public static EquipManager Equip { get { return Instance._equip; } }
    public static SkillManager Skill { get { return Instance._skill; } }

    void Start()
    {
        init(); 
    }

    void Update()
    {
        _input.OnUpdate();
    }

    public static void init()
    {
        if(s_Instance == null)
        {
            GameObject go = GameObject.Find("@Managers");
            if(go == null)
            {
                go = new GameObject { name = "@Managers" };
                go.AddComponent<Managers>();
            }

            s_Instance = go.GetComponent <Managers>();
            DontDestroyOnLoad(go);

            s_Instance._pool.Init();

            //소리 초기화
            s_Instance._data.Init();
            s_Instance._sound.Init();
            s_Instance._pool.Init();
        }
    }


    public static void Clear()
    {
        Sound.Clear();
        Input.Clear();
        Scene.Clear();
        UI.Clear();
        Pool.Clear();
    }
}
