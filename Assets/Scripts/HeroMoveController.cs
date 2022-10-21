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

public struct PosDir        //헤드 히어로가 방향 변경 시의 위치값과 방향값을 담아둘 구조체
{
    public Vector2 pos;
    public Direction dir;
}

public class HeroMoveController : MonoBehaviour
{
    public int idx { get; private set; }    //영웅몇번째 자리인지 알려줄 인덱스

    [SerializeField] protected float speed = 1;     //  protected 상속 받을 때 사용하려고 함

    Rigidbody2D rb;

    public bool isHead { get; private set; }        //해당 영웅이 머리인지 아닌지를 판단할 변수
    public Direction dir { get; private set; }      //영웅의 이동 방향

    List<PosDir> posDir = new List<PosDir>();
    public List<PosDir> GET_POS_DIR { get { return posDir; } }

    bool isTurn = false;        //턴 상태 체크. posDir 리스트에 데이터가 있다면 턴 중인 상태
    bool isArrive = false;      //타겟 위치까지 이동했는지 판단할 변수

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
        if(posDir.Count != 0)   //posDir 리스트 안에 값이 있다면
        {
            isTurn = true;      //턴 상태 true

            if (dir == Direction.UP)
            {
                if (transform.position.y >= posDir[0].pos.y)     //영웅의 위치가 저장된 위치값과 같아지거나 지나친다면
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
                transform.position = posDir[0].pos; //위치 보정
                Move(posDir[0].dir);                //방향 변경
                posDir.RemoveAt(0);                 //데이터 삭제
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

        posDir.Add(temp);       //위치와 방향 리스트에 저장
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
                                                                                //float에 있는 값이 0.5보다 낮으면 0/float에 있는 값이0.5보다 높으면 1이되는 반올림
                                                                                //영웅들의 이미지를 y축 값에 따라 order 설정. y축 값이 마이너스면 이미지의 order가 더 높다
            float y = transform.position.y * 10;
            sr.sortingOrder = (int)(-1f * y);
        }
    }
}
