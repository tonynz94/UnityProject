using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers s_Instance; //매니저는 static
    public static Managers Instance { get { init(); return s_Instance; } }

    GameManager _game = new GameManager();
    public static GameManager Game { get {  return Instance._game; } }

    InputManager _input = new InputManager();
    public static InputManager Input { get { return Instance._input; } }

    ResourceManager _resource = new ResourceManager();
    public static ResourceManager Resource { get { return Instance._resource; } }

    UIManager _ui = new UIManager();
    public static UIManager UI { get { return Instance._ui; } }

    SceneManagerEx _scene = new SceneManagerEx();
    public static SceneManagerEx Scene { get { return Instance._scene; } }

    SoundManager _sound = new SoundManager();
    public static SoundManager Sound { get { return Instance._sound; } }

    PoolManager _pool = new PoolManager();
    public static PoolManager Pool { get { return Instance._pool; } }

    DataManager _data = new DataManager();
    public static DataManager Data { get { return Instance._data; } }

    static int num = 0;

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
