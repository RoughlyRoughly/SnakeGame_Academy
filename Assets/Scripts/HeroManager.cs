using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroManager : MonoBehaviour
{
    public static HeroManager i;

    [SerializeField] GameObject[] prefabHeroes;     //������ �������� ��Ƶ� �迭
    [SerializeField] float inputRate = 0.2f;        //�̵� Ű�Է� ������
    float inputTime = 0;
    bool isCanInput = false;

    [SerializeField] HeroMoveController headHero;   //�Ӹ� ������ ��Ƶ� ����
    public Transform HEAD_POS { get { return headHero.transform; } }

    [SerializeField] List<HeroMoveController> heroList = new List<HeroMoveController>();
    
    public List<HeroMoveController> LIST { get { return heroList; } }

    // Start is called before the first frame update
    void Start()
    {
        i = this;
        CreateHead();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isCanInput)
        {
            inputTime += Time.deltaTime;
            if (inputTime >= inputRate) isCanInput = true;
        }
        else
        {
            InputKey();
        }
    }

    void CreateHead()
    {
        headHero = transform.GetChild(0).GetChild(0).GetComponent<HeroMoveController>();
        headHero.SetHead();             //���� ������ �Ӹ��� ����
        CameraManager.i.SetHeadHero(headHero.transform);            //ī�޶� ��� ����� ����
        heroList.Add(headHero);         //����Ʈ�� ���, ����� �߰�
        headHero.Move(Direction.UP);    //���� �̵�
    }

    void ChangeDirection(Direction _dir)
    {
        headHero.Move(_dir);        //�Ӹ����� ���⺯��

        inputTime = 0;              //Ű�Է� üũ �ð� �ʱ�ȭ

        isCanInput = false;
    }

    void InputKey()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (headHero.dir == Direction.UP || headHero.dir == Direction.DOWN) return;

            ChangeDirection(Direction.UP);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            if (headHero.dir == Direction.UP || headHero.dir == Direction.DOWN) return;

            ChangeDirection(Direction.DOWN);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            if (headHero.dir == Direction.LEFT || headHero.dir == Direction.RIGHT) return;

            ChangeDirection(Direction.LEFT);
        }
        else if(Input.GetKeyDown(KeyCode.D))
        {
            if (headHero.dir == Direction.RIGHT || headHero.dir == Direction.LEFT) return;

            ChangeDirection(Direction.RIGHT);
        }
    }
}
