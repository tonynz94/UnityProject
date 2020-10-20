using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawningPool : MonoBehaviour
{
    [SerializeField]
    int _monsterCount = 0;
    int _reserveCount = 0;

    //유지 시켜야 하는 몬스터 개체 수
    [SerializeField]
    int _keepMonsterCount = 0;

    [SerializeField]
    Vector3 _spawnPos;

    //spawn 기준으로 radius안에 랜덤으로 스폰
    [SerializeField]
    float _spawnRadius = 15.0f;

    //새로 스폰이 되는 타임
    [SerializeField]
    float _spawnTime = 5.0f;
    // Start is called before the first frame update
    public void AddMonsterCount(int value) { _monsterCount += value; }
    public void SetKeepMonsterCount(int count) { _keepMonsterCount = count; }
    void Start()
    {
        Managers.Game.OnSpawnEvent -= AddMonsterCount;
        Managers.Game.OnSpawnEvent += AddMonsterCount;
    }

    // Update is called once per frame
    void Update()
    {
        while (_reserveCount +_monsterCount < _keepMonsterCount)
        {     
            StartCoroutine(ReserveSpawn());         
        }
    }

    IEnumerator ReserveSpawn()
    {
        _reserveCount++; //0, 1, 2, 3, 4, 5,
        yield return new WaitForSeconds(Random.Range(1.0f,_spawnTime));
        GameObject go = Managers.Game.Spawn(Define.WorldObject.Monster, "Knight");
        NavMeshAgent nma = go.GetOrAddComponent<NavMeshAgent>();

        Vector3 randPos;

       
        while (true)
        {
            //원을 그려서 랜덤좌표를 가져옴.
            Vector3 randDir = Random.insideUnitSphere * _spawnRadius;
            randDir.y = 0;
            randPos = _spawnPos + randDir;

            //네비로 갈 수 있는 영역인지 확인하는 것 
            //갈 수 있다면 true
            NavMeshPath path = new NavMeshPath();
            if (nma.CalculatePath(randPos, path))
                break;
        }
        _reserveCount--; //0, 1, 2, 3, 4, 5,
        go.transform.position = randPos;
    }
}
