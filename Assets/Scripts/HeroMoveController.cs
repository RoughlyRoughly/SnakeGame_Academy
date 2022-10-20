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
