using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager
{
    #region Pool
    class Pool
    {
        public GameObject Original { get; private set; }
        public Transform Root { get; set; }

        Stack<Poolable> _poolStack = new Stack<Poolable>();

        //오브젝트를 풀을 만들어주면 기본적으로 5개의 오브젝트를 생성합니다.
        public void Init(GameObject original , int count = 5)
        {
            //원본 오브젝트의 이름으로 해당 오브젝트를 묶어줄 Root 생성합니다.
            Original = original;
            Root = new GameObject() { name = $"{original.name}_Root" }.transform;

            //Root오브젝트에 원하는 갯수만큼 오브젝트를 생성해 줍니다.
            //poolable 스크립트로 stack에 저장해 줌.
            for (int i = 0; i < count; i++)
                Push(Create());
        }

        Poolable Create()
        {
            //원본을 제공하여 go에 복사본을 저장
            GameObject go = Object.Instantiate<GameObject>(Original);
            go.name = Original.name;

            //해당 go에 Poolable 추가 또는 가져 옴
            return go.GetOrAddComponent<Poolable>();
        }

        //pool에 저장하지만 오브젝트가 자고있는 상태로 설정.
        public void Push(Poolable poolable)
        {
            if (poolable == null)
                return;

            poolable.transform.parent = Root;
            poolable.gameObject.SetActive(false);
            poolable.isUsing = false;

            _poolStack.Push(poolable);
        }

        //자고있는 오브젝트를 뽑아서 쓸때 사용하는 함수
        public Poolable Pop(Transform parent)
        {
            Poolable poolable;

            //pool에 갯수가 충분히 있으면 뽑아오고
            if (_poolStack.Count > 0)
                poolable = _poolStack.Pop();

            else //없으면 새로 한개를 더 만듬.
                poolable = Create();

            //뽑아서 가져온 오브젝트는 활성화 시켜줍니다.
            poolable.gameObject.SetActive(true);
            //활성화 시켜줄때 부모클래스를 정의해주지 않았으면
            //현재 씬 오브젝트에 성생해줍니다.
            if (parent == null)
                poolable.transform.parent = Managers.Scene.CurrentScene.transform;

            
            poolable.transform.parent = parent;
            poolable.isUsing = true;

            return poolable;
        }
    }
    #endregion

    //오브젝트 폴링 대상의 모든 오브젝트를 관리해주는 딕셔너리
    Dictionary<string, Pool> _pool = new Dictionary<string, Pool>();
    Transform _root;

    //게임오브젝트 @Pool_Root(비어있음) 생성.
    public void Init()
    {
        if (_root == null)
        {
            _root = new GameObject { name = "@Pool_Root" }.transform;
            Object.DontDestroyOnLoad(_root);

        }
    }
    
    //풀링 대상인 오브젝트 딕셔너리에 저장해주는 함수. 
    public void CreatePool(GameObject original, int count = 5)
    {
        //pool클래스에 풀링 대상 오브젝트 갯수 만큼 만들어 줌.
        Pool pool = new Pool();
        pool.Init(original, count);

        pool.Root.parent = _root.transform;

        _pool.Add(original.name, pool); 
    }

    //딕셔너리에 관리되고있는 폴링 오브젝트를 다시 반환해주는 역할을 한다.
    public void Push(Poolable poolable)
    {
        string name = poolable.gameObject.name;

        //pop을 한번도 안하고 push를 한 상황. (아주 가끔 나올 수 있는 예외 상황)
        if( _pool.ContainsKey(name) == false)
        {
            GameObject.Destroy(poolable.gameObject);
            return;
        }
        _pool[name].Push(poolable);
    }

    //딕셔너리에 관리되고있는 폴링 오브젝트를 사용하기 위해 가져오는 역할을 한다.
    public Poolable Pop(GameObject original, Transform parent = null)
    {
        //Dirtionary에 "Unity_Chan"이 포함 되어 있는지 확인 없다면.
        if (_pool.ContainsKey(original.name) == false)
            CreatePool(original);   //pool을 생성해줌.

        return _pool[original.name].Pop(parent);
    }

    //원본을 가져오는 것.
    public GameObject GetOriginal(string name)
    {
        //한번도 안 만들었다면.
        if (_pool.ContainsKey(name) == false)
            return null;

        //만들어서 pool에 존재한다면
        return _pool[name].Original;
    }

    //씬에서 다른 씬으로 넘어갈 시.
    //@Pool_Root child에 다 포함되어 있음.
    public void Clear()
    {
        foreach (Transform child in _root)
            GameObject.Destroy(child.gameObject);

        _pool.Clear();
    }
}
