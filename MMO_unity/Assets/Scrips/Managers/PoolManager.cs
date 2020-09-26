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

        public void Init(GameObject original , int count = 5)
        {
            Original = original;
            Root = new GameObject().transform;
            Root.name = $"{original.name}_Root";

            for (int i = 0; i < count; i++)
                Push(Create()); //poolable 스크립트로 stack에 저장해 줌.
        }

        Poolable Create()
        {
            //go라는 복사본을 만듬
            GameObject go = Object.Instantiate<GameObject>(Original);
            go.name = Original.name;

            //go에 Poolable script 추가 함
            return go.GetOrAddComponent<Poolable>();
        }

        public void Push(Poolable poolable)
        {
            if (poolable == null)
                return;

            poolable.transform.parent = Root;
            poolable.gameObject.SetActive(false);
            poolable.isUsing = false;

            _poolStack.Push(poolable);
        }

        public Poolable Pop(Transform parent)
        {
            Poolable poolable;

            //pop으로 꺼내 와줌.
            if (_poolStack.Count > 0)
                poolable = _poolStack.Pop();
            else
                poolable = Create();

            //활성화 시켜준다.
            poolable.gameObject.SetActive(true);
            if (parent == null)
                poolable.gameObject.transform.parent = Managers.Scene.CurrentScene.transform;

            poolable.transform.parent = parent;
            poolable.isUsing = true;

            return poolable;
        }
    }
    #endregion

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
        Pool pool = new Pool();
        pool.Init(original, count); //stack에 poolable로 저장 완료.
        pool.Root.parent = _root.transform; //UnityChan_Pool를 @Pool_Root의 자식으로 넣어 줌

        _pool.Add(original.name, pool); //딕셔너리에 추가 해줌
    }

    //사용을 다 하고 다시 Pool에 넣어 줄때 사용.
    public void Push(Poolable poolable)     //poolable을 가지고 있어야 함 
    {
        string name = poolable.gameObject.name;

        if( _pool.ContainsKey(name) == false)   //pool관리 대상이 아니라면
        {
            GameObject.Destroy(poolable.gameObject);
            return;
        }
        _pool[name].Push(poolable);
    }

    public Poolable Pop(GameObject original, Transform parent = null)
    {
        //Dirtionary에 "Unity_Chan"이 포함 되어 있는지 확인 없다면.
        if (_pool.ContainsKey(original.name) == false)
            CreatePool(original);   //pool을 생성해줌.

        return _pool[original.name].Pop(parent);
    }

    public GameObject GetOriginal(string name)
    {
        if (_pool.ContainsKey(name) == false)
            return null;
        return _pool[name].Original;
    }

    public void Clear()
    {
        foreach (Transform child in _root)
            GameObject.Destroy(child.gameObject);

        _pool.Clear();
    }
}
