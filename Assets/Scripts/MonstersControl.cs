using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonstersControl : MonoBehaviour
{
    List<MonsterMoveController> monList = new List<MonsterMoveController>();    //���͵��� ��Ƶ� ����Ʈ
    public List<MonsterMoveController> MON_LIST { get { return monList; } }

    [SerializeField] MonsterMoveController headMon;     //�Ӹ� ����

    bool isTurn = false;
    float turnTime = 0;


    //���͵��� �̵� ��� ����

    [SerializeField] float minLimitX = 0;
    [SerializeField] float maxLimitX = 0;

    [SerializeField] float minLimitY = 0;
    [SerializeField] float maxLimitY = 0;

    Vector2 pos;

    // Start is called before the first frame update
    void Start()
    {
        turnTime = Random.Range(3, 7);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isTurn)
        {
            turnTime -= Time.deltaTime;

            if (turnTime <= 0)
            {
                isTurn = true;
                ChangeMoveDirection(RandomDirection());
            }
        }
        MoveLimit();
    }

    public void SetHead(MonsterMoveController head)
    {
        headMon = head;
    }

    void ChangeMoveDirection(Direction _dir)
    {
        headMon.Move(_dir);
        Vector2 pos = headMon.transform.position;
        
        if (monList.Count > 1)
        {
            for(int i = 1; i < monList.Count; i++)
            {
                monList[i].AddPosDir(pos, _dir);
                //���� ���͵��� posDir ����Ʈ�� �Ӹ� ���� ��ġ�� ������ ����
            }
        }

        turnTime = Random.Range(3, 7);      //�� Ÿ�� �ʱ�ȭ
        isTurn = false;
    }

    Direction RandomDirection()
    {
        Direction dir;

        while (true)
        {
            dir = (Direction)Random.Range(0,4); //������ ���� ����

            if(dir == Direction.UP)
            {
                if (headMon.dir == Direction.UP || headMon.dir == Direction.DOWN ||
                    headMon.transform.position.y >= maxLimitY - 0.5f) continue;
                else break;
            }
            else if (dir == Direction.DOWN)
            {
                if (headMon.dir == Direction.UP || headMon.dir == Direction.DOWN ||
                    headMon.transform.position.y <= minLimitY + 0.5f) continue;
                else break;
            }
            else if (dir == Direction.LEFT)
            {
                if (headMon.dir == Direction.LEFT || headMon.dir == Direction.RIGHT ||
                    headMon.transform.position.x <= minLimitX + 0.5f) continue;
                else break;
            }
            else if (dir == Direction.RIGHT)
            {
                if (headMon.dir == Direction.LEFT || headMon.dir == Direction.RIGHT ||
                    headMon.transform.position.x >= maxLimitX - 0.5f) continue;
                else break;
            }
        }
        return dir;     //������ ������ ��ȯ����
    }

    void MoveLimit()
    {
        if (headMon != null) pos = headMon.transform.position;      //�Ӹ� ������ ��ġ�� �����

        if(headMon.dir == Direction.UP)
        {
            if(pos.y >= maxLimitY)
            {
                isTurn = true;

                if (pos.x <= minLimitX + 0.2f) ChangeMoveDirection(Direction.RIGHT);            //���� ��ġ�� ���� �� ������ ��
                else if (pos.x >= maxLimitX - 0.2f) ChangeMoveDirection(Direction.LEFT);        //���� ��ġ�� ������ �� ������ ��
                else ChangeMoveDirection((Direction)Random.Range(2, 4));
            }
        }
        else if(headMon.dir == Direction.DOWN)
        {
            if (pos.y <= minLimitY)
            {
                isTurn = true;

                if (pos.x <= minLimitX + 0.2f) ChangeMoveDirection(Direction.RIGHT);            //���� ��ġ�� ���� �Ʒ� ������ ��
                else if (pos.x >= maxLimitX - 0.2f) ChangeMoveDirection(Direction.LEFT);        //���� ��ġ�� ������ �Ʒ� ������ ��
                else ChangeMoveDirection((Direction)Random.Range(2, 4));
            }
        }
        else if (headMon.dir == Direction.LEFT)
        {
            if (pos.x <= minLimitX)
            {
                isTurn = true;

                if (pos.y <= minLimitY + 0.2f) ChangeMoveDirection(Direction.UP);               //���� ��ġ�� ���� �Ʒ� ������ ��
                else if (pos.y >= maxLimitY - 0.2f) ChangeMoveDirection(Direction.DOWN);        //���� ��ġ�� ���� �� �Ʒ� ������ ��
                else ChangeMoveDirection((Direction)Random.Range(0, 2));
            }
        }
        else if (headMon.dir == Direction.RIGHT)
        {
            if (pos.x >= maxLimitX)
            {
                isTurn = true;

                if (pos.y <= minLimitY + 0.2f) ChangeMoveDirection(Direction.UP);               //���� ��ġ�� ������ �Ʒ� ������ ��
                else if (pos.y >= maxLimitY - 0.2f) ChangeMoveDirection(Direction.DOWN);        //���� ��ġ�� ������ �� ������ ��
                else ChangeMoveDirection((Direction)Random.Range(0, 2));
            }
        }
    }
}
