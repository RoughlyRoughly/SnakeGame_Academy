using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawnComponent : MonoBehaviour
{
    //���� ���� ����
    [SerializeField] float minLimitX = 0;
    [SerializeField] float maxLimitX = 0;
    [SerializeField] float minLimitY = 0;
    [SerializeField] float maxLimitY = 0;

    [SerializeField] float spawnRate = 3;   //����  ���� ����
    float t = 0;

    [SerializeField] GameObject monsterGroupPrefab;     //������ ���͵��� ������ �׷� ������
    [SerializeField] GameObject[] monstersPrefab;        //������ ���͵��� ��Ƶ� �迭

    [SerializeField] float offset = 1.5f;               //���� ���� ����
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
        Direction dir = (Direction) Random.Range(0, 4); //���� ����
        Vector2 pos = new Vector2(Random.Range(minLimitX, maxLimitX),Random.Range(minLimitY, maxLimitY));     //���� ��ġ ����

        GameObject group = Instantiate(monsterGroupPrefab, transform);      //���� �׷� ������Ʈ ����

        int monLength = Random.Range(1, 4);     //1~3������ ���͸� ����

        for (int i = 0; i < monLength; i++)
        {
            GameObject mon = Instantiate(monstersPrefab[Random.Range(0, monstersPrefab.Length)], group.transform);    //���� ����

            GameObject _hpBar = Instantiate(hpBarPrefab, hpBarTrans);
            mon.GetComponent<MonsterComponent>().SetHpBar(_hpBar);

            if(i == 0)
            {
                mon.transform.position = pos;                                                                   //������ ��ġ�� �̵�
                mon.GetComponent<MonsterMoveController>().SetHead();                                            //�Ӹ� ����
                mon.GetComponent<MonsterMoveController>().Move(dir);                                            //�̵�
                group.GetComponent<MonstersControl>().SetHead(mon.GetComponent<MonsterMoveController>());       //�׷쿡 �Ӹ� ����
            }
            else
            {
                MonsterMoveController preMon = group.GetComponent<MonstersControl>().MON_LIST[i - 1];           //���� ������ ���� �ε��� -1�� ���͸� ����Ʈ���� ������
                FollowDirection(preMon, mon);
            }
            group.GetComponent<MonstersControl>().MON_LIST.Add(mon.GetComponent<MonsterMoveController>());      //���� ����Ʈ�� ������ ���� �߰�
        }

        t = 0;  //���� �ð� �ʱ�ȭ
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
