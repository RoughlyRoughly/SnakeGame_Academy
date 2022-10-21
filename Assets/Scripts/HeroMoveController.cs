using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    UP,
    DOWN,
    LEFT,
    RIGHT
}

public struct PosDir        //��� ����ΰ� ���� ���� ���� ��ġ���� ���Ⱚ�� ��Ƶ� ����ü
{
    public Vector2 pos;
    public Direction dir;
}

public class HeroMoveController : MonoBehaviour
{
    public int idx { get; private set; }    //�������° �ڸ����� �˷��� �ε���

    [SerializeField] protected float speed = 1;     //  protected ��� ���� �� ����Ϸ��� ��

    Rigidbody2D rb;

    public bool isHead { get; private set; }        //�ش� ������ �Ӹ����� �ƴ����� �Ǵ��� ����
    public Direction dir { get; private set; }      //������ �̵� ����

    List<PosDir> posDir = new List<PosDir>();
    public List<PosDir> GET_POS_DIR { get { return posDir; } }

    bool isTurn = false;        //�� ���� üũ. posDir ����Ʈ�� �����Ͱ� �ִٸ� �� ���� ����
    bool isArrive = false;      //Ÿ�� ��ġ���� �̵��ߴ��� �Ǵ��� ����

    Animator anit;
    SpriteRenderer sr;

    void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
    }

    void Update()
    {
        MoveDestination();
        SetOrderLayer();
    }

    public void Move(Direction _dir)
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        if (anit == null) anit = GetComponentInChildren<Animator>();

        dir = _dir;
        if (dir == Direction.UP)
        {
            rb.velocity = Vector2.up * speed;
        }
        else if (dir == Direction.DOWN)
        {
            rb.velocity = Vector2.down * speed;
        }
        else if (dir == Direction.LEFT)
        {
            rb.velocity = Vector2.left * speed;
        }
        else if(dir == Direction.RIGHT)
        {
            rb.velocity = Vector2.right * speed;
        }

        SetAnim();
    }

    void MoveDestination()
    {
        if(posDir.Count != 0)   //posDir ����Ʈ �ȿ� ���� �ִٸ�
        {
            isTurn = true;      //�� ���� true

            if (dir == Direction.UP)
            {
                if (transform.position.y >= posDir[0].pos.y)     //������ ��ġ�� ����� ��ġ���� �������ų� ����ģ�ٸ�
                {
                    isArrive = true;
                }
            }
            else if (dir == Direction.DOWN)
            {
                if (transform.position.y <= posDir[0].pos.y)
                {
                    isArrive = true;
                }
            }
            else if (dir == Direction.LEFT)
            {
                if (transform.position.x <= posDir[0].pos.x)
                {
                    isArrive = true;
                }
            }
            else if(dir == Direction.RIGHT)
            {
                if(transform.position.x >= posDir[0].pos.x)
                {
                    isArrive = true;
                }
            }

            if (isArrive)
            {
                isArrive = false;
                transform.position = posDir[0].pos; //��ġ ����
                Move(posDir[0].dir);                //���� ����
                posDir.RemoveAt(0);                 //������ ����
            }
        }
        else
        {
            if (isTurn) isTurn = false;
        }
    }

    public void AddPosDir(Vector2 _pos, Direction _dir)
    {
        PosDir temp;
        temp.pos = _pos;
        temp.dir = _dir;

        posDir.Add(temp);       //��ġ�� ���� ����Ʈ�� ����
    }

    public void SetHead()
    {
        isHead = true;
    }

    protected void SetAnim()
    {
        if (dir == Direction.UP) anit.SetTrigger("Up");
        else if (dir == Direction.DOWN) anit.SetTrigger("Down");
        else if (dir == Direction.LEFT) anit.SetTrigger("Left");
        else anit.SetTrigger("Right");
    }

    void SetOrderLayer()
    {
        if(dir == Direction.UP || dir == Direction.DOWN)
        {
            sr.sortingOrder = -1 * Mathf.RoundToInt(transform.position.y);      //Mathf.RoundToInt:
                                                                                //float�� �ִ� ���� 0.5���� ������ 0/float�� �ִ� ����0.5���� ������ 1�̵Ǵ� �ݿø�
                                                                                //�������� �̹����� y�� ���� ���� order ����. y�� ���� ���̳ʽ��� �̹����� order�� �� ����
            float y = transform.position.y * 10;
            sr.sortingOrder = (int)(-1f * y);
        }
    }
}
