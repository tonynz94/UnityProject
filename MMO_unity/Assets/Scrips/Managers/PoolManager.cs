using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager
{
    //Unity_Chan_Root 
    #region Pool
    class Pool
    {
        public GameObject Original { get; private set; }
        public Transform Root { get; set; }

        //Unity_Chan_Root 오브젝트 자식으로 달려있는 오브젝드들이 가지고 있는 컴포넌트
        Stack<Poolable> _poolStack = new Stack<Poolable>();

        //count 5개의 오브젝트
        public void Init(GameObject original , int count = 5)
        {
            Original = original;
            Root = new GameObject() { name = $"{original.name}_Root" }.transform;

            for (int i = 0; i < count; i++)
                Push(Create()); //poolable 스크립트로 stack에 저장해 줌.
        }

        Poolable Create()
        {
            //원본을 제공하여 go에 복사본을 저장
            GameObject go = Object.Instantiate<GameObject>(Original);
            go.name = Original.name;

            //해당 go에 Poolable 추가
            return go.GetOrAddComponent<Poolable>();
        }

        //pool에 저장하지만 오브젝트가 자고있는 상태.
        public void Push(Poolable poolable)
        {
            if (poolable == null)
                return;

            poolable.transform.parent = Root;
            poolable.gameObject.SetActive(false);
            poolable.isUsing = false;

            _poolStack.Push(poolable);
        }

        //pool에서 사용하는것.
        public Poolable Pop(Transform parent)
        {
            Poolable poolable;

            //pool에 갯수가 충분히 있으면 뽑아오고
            if (_poolStack.Count > 0)
                poolable = _poolStack.Pop();
            else //없으면 새로 만듬.
                poolable = Create();

            //활성화 시켜준다.
            poolable.gameObject.SetActive(true);
            if (parent == null)
                poolable.gameObject.transform.parent = Managers.Scene.CurrentScene.transform;

            if (parent == null)
                poolable.transform.parent = Managers.Scene.CurrentScene.transform;

            Debug.Log(poolable.transform.parent.name);

            poolable.transform.parent = parent;
            poolable.isUsing = true;

            return poolable;
        }
    }
    #endregion

    //Pool_Root 
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
    
    //오브젝트를 pool으로 관리 하면 default값으로 다섯개를 생성 함.
    public void CreatePool(GameObject original, int count = 5)
    {
        //새로운 오브젝트 5개를 만들고 pool에서 오브젝트 만듬것을 만들고
        Pool pool = new Pool();
        pool.Init(original, count);
        //pool_root연결
        pool.Root.parent = _root.transform;

        _pool.Add(original.name, pool); //딕셔너리에 추가 해줌
    }

    //다 사용후 반환하는 것.
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

    //pooling된 오브젝트가 있는 확인 후 있으면 바로 사용
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
