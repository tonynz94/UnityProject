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

    void Update()
    {
        //유지 시켜야 하는 몬스터 개체 수 만큼 반복시킴
        //True -> 몬스터를 더 생성하는 것
        while (_reserveCount +_monsterCount < _keepMonsterCount)
        {     
            StartCoroutine(ReserveSpawn());         
        }
    }

    //몬스터를 스폰해주는 함수
    IEnumerator ReserveSpawn()
    {
        //몬스터를 생성하지는 않았지만 예약을 일단 함.
        _reserveCount++; 
        yield return new WaitForSeconds(Random.Range(1.0f,_spawnTime));

        //몬스터를 실질적으로 생성해줍니다. (여기서 AddMonsterCount의 값을1 증가 시켜 줌.
        GameObject go = Managers.Game.Spawn(Define.WorldObject.Monster, "Knight");

        //직전에 생성한 오브젝트에 NavMeshAgent가 추가 또는 가져와줍니다.
        NavMeshAgent nma = go.GetOrAddComponent<NavMeshAgent>();

        Vector3 randPos;

        while (true)
        {
            yield return null;
            //sectionA지역에_spawnRadius 지름의 원을 그려 랜덤좌표를 찍음.
            Vector3 randDir = Random.insideUnitSphere * _spawnRadius + Define.sectionA;

            //상공에서 Raycast를 쏘기 위해 높이를 임시로 100으로 해줌.
            randDir.y = 100.0f;
            
            RaycastHit hit;
            //맵의 높이가 다 다름으로 상공에서 Raycast를 쏴서 높이를 구하는 것
            Debug.DrawRay(randDir, Vector3.down * 100.0f ,Color.red,1);    
            //True -> Raycast가 땅에 부딪쳤을 시. 
            //False -> 땅이 아닌 나무 위, 돌 위등 다른 오브젝트가 부딪친 것(몬스터를 스폰하지 않음)
            if (Physics.Raycast(randDir, Vector3.down, out hit,100.0f, LayerMask.GetMask("Ground")))
            {
                //높이를 구해주고 해당 높이로 설정
                randPos = hit.point;
                randPos.y = randPos.y + 0.3f;
                break;                          
            }
        }
        _reserveCount--; //0, 1, 2, 3, 4, 5,
        go.transform.position = randPos;
    }
}
