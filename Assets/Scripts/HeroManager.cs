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

    [SerializeField] float offset = 0.8f;       //�������� ����

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
        Debug.Log(headHero);

        Debug.Log(/*CameraManager.i*/ CameraManager.i);
        headHero = transform.GetChild(0).GetChild(0).GetComponent<HeroMoveController>();
        headHero.SetHead();             //���� ������ �Ӹ��� ����
        /*CameraManager.i*/
        CameraManager.i.SetHeadHero(headHero.transform);            //ī�޶� ��� ����� ����

        heroList.Add(headHero);         //����Ʈ�� ���, ����� �߰�
        headHero.Move(Direction.UP);    //���� �̵�
    }

    void ChangeDirection(Direction _dir)
    {
        headHero.Move(_dir);        //�Ӹ����� ���⺯��
        Vector2 pos = headHero.transform.position;

        if (heroList.Count > 1)
        {
            for (int i = 1; i < heroList.Count; i++)
            {
                heroList[i].AddPosDir(pos, _dir);       //�Ӹ� ������ ��ġ�� ���� �� ���� ������ ����Ʈ�� ����
            }
        }
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
        else if (Input.GetKeyDown(KeyCode.D))
        {
            if (headHero.dir == Direction.RIGHT || headHero.dir == Direction.LEFT) return;

            ChangeDirection(Direction.RIGHT);
        }
    }

    public void AddHero(int id)
    {
        HeroMoveController preHero = heroList[heroList.Count - 1];      //�߰��� ���� ���� ���� ������

        Vector2 pos = Vector2.zero;                     //�߰��� ������ ��ġ
        Vector2 pPos = preHero.transform.position;      //�� ������ ��ġ

        //�߰��� ������ ��ġ ����
        if (preHero.dir == Direction.UP) pos = new Vector2(pPos.x, pPos.y - offset);
        else if (preHero.dir == Direction.DOWN) pos = new Vector2(pPos.x, pPos.y + offset);
        else if (preHero.dir == Direction.LEFT) pos = new Vector2(pPos.x + offset, pPos.y);
        else if (preHero.dir == Direction.RIGHT) pos = new Vector2(pPos.x - offset, pPos.y);

        GameObject temp = Instantiate(prefabHeroes[id], pos, Quaternion.identity, transform.GetChild(0));   //���� ����

        HeroMoveController hero = temp.GetComponent<HeroMoveController>();

        for (int i = 0; i < preHero.GET_POS_DIR.Count; i++)
        {
            hero.AddPosDir(preHero.GET_POS_DIR[i].pos, preHero.GET_POS_DIR[i].dir);
            //�� ������ posDir ����Ʈ�� �����͸� ������ ���� ����Ʈ���� �߰�����  
        }

        hero.Move(preHero.dir);         //������ ���� �̵�
        heroList.Add(hero);             //������ ���� ����Ʈ�� �߰�
    }
}
