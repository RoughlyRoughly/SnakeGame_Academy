using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawnComponent : MonoBehaviour
{
    //몬스터 스폰 영역
    [SerializeField] float minLimitX = 0;
    [SerializeField] float maxLimitX = 0;
    [SerializeField] float minLimitY = 0;
    [SerializeField] float maxLimitY = 0;

    [SerializeField] float spawnRate = 3;   //몬스터  스폰 간격
    float t = 0;

    [SerializeField] GameObject monsterGroupPrefab;     //생성할 몬스터들을 제엉할 그룹 프리팹
    [SerializeField] GameObject[] monstersPrefab;        //생성할 몬스터들을 담아둘 배열

    [SerializeField] float offset = 1.5f;               //몬스터 간의 간격
    [SerializeField] GameObject hpBarPrefab;
    Transform hpBarTrans;

    // Start is called before the first frame update
    void Start()
    {
        hpBarTrans = GameObject.Find("MonsterHpBars").transform;
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;
        if (t >= spawnRate) SpawnMonster();
    }

    void SpawnMonster()
    {
        Direction dir = (Direction) Random.Range(0, 4); //방향 설정
        Vector2 pos = new Vector2(Random.Range(minLimitX, maxLimitX),Random.Range(minLimitY, maxLimitY));     //생성 위치 설정

        GameObject group = Instantiate(monsterGroupPrefab, transform);      //몬스터 그룹 오브젝트 생성

        int monLength = Random.Range(1, 4);     //1~3마리의 몬스터를 생성

        for (int i = 0; i < monLength; i++)
        {
            GameObject mon = Instantiate(monstersPrefab[Random.Range(0, monstersPrefab.Length)], group.transform);    //몬스터 생성

            GameObject _hpBar = Instantiate(hpBarPrefab, hpBarTrans);
            mon.GetComponent<MonsterComponent>().SetHpBar(_hpBar);

            if(i == 0)
            {
                mon.transform.position = pos;                                                                   //설정한 위치로 이동
                mon.GetComponent<MonsterMoveController>().SetHead();                                            //머리 설정
                mon.GetComponent<MonsterMoveController>().Move(dir);                                            //이동
                group.GetComponent<MonstersControl>().SetHead(mon.GetComponent<MonsterMoveController>());       //그룹에 머리 설정
            }
            else
            {
                MonsterMoveController preMon = group.GetComponent<MonstersControl>().MON_LIST[i - 1];           //현재 생성될 몬스터 인덱스 -1한 몬스터를 리스트에서 가져옴
                FollowDirection(preMon, mon);
            }
            group.GetComponent<MonstersControl>().MON_LIST.Add(mon.GetComponent<MonsterMoveController>());      //몬스터 리스트에 생성한 몬스터 추가
        }

        t = 0;  //스폰 시간 초기화
    }

    void FollowDirection(MonsterMoveController preMon, GameObject mon)
    {
        Vector2 prePos = preMon.transform.position;

        if (preMon.dir == Direction.UP) mon.transform.position = new Vector2(prePos.x, prePos.y - offset);
        else if (preMon.dir == Direction.DOWN) mon.transform.position = new Vector2(prePos.x, prePos.y + offset);
        else if (preMon.dir == Direction.LEFT) mon.transform.position = new Vector2(prePos.x + offset, prePos.y);
        else if (preMon.dir == Direction.RIGHT) mon.transform.position = new Vector2(prePos.x - offset , prePos.y);

        mon.GetComponent<MonsterMoveController>().Move(preMon.dir);
    }
}
