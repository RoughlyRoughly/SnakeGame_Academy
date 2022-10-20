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

    Animator anit;


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
}
