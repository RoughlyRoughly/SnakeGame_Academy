using System.Collections.Generic;
using UnityEngine;

public class HeroManager : MonoBehaviour
{
    public static HeroManager i;

    [SerializeField] GameObject[] prefabHeroes;     //생성한 영웅들을 담아둘 배열

    [SerializeField] float inputRate = 0.2f;        //이동 키입력 딜레이
    float inputTime = 0;
    bool isCanInput = false;

    [SerializeField] HeroMoveController headHero;   //머리 영웅을 담아둘 변수
    public Transform HEAD_POS { get { return headHero.transform; } }

    [SerializeField] List<HeroMoveController> heroList = new List<HeroMoveController>();

    public List<HeroMoveController> LIST { get { return heroList; } }

    [SerializeField] float offset = 0.8f;       //영웅간의 간격

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
        headHero.SetHead();             //현재 영웅을 머리로 세팅
        /*CameraManager.i*/
        CameraManager.i.SetHeadHero(headHero.transform);            //카메라에 헤드 히어로 연결

        heroList.Add(headHero);         //리스트에 헤드, 히어로 추가
        headHero.Move(Direction.UP);    //영웅 이동
    }

    void ChangeDirection(Direction _dir)
    {
        headHero.Move(_dir);        //머리영웅 방향변경
        Vector2 pos = headHero.transform.position;

        if (heroList.Count > 1)
        {
            for (int i = 1; i < heroList.Count; i++)
            {
                heroList[i].AddPosDir(pos, _dir);       //머리 영웅의 위치와 방향 각 꼬리 영웅들 리스트에 저장
            }
        }
        inputTime = 0;              //키입력 체크 시간 초기화
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
        HeroMoveController preHero = heroList[heroList.Count - 1];      //추가될 영웅 앞의 영웅 가져옴

        Vector2 pos = Vector2.zero;                     //추가될 영웅의 위치
        Vector2 pPos = preHero.transform.position;      //앞 영웅의 위치

        //추가될 영웅의 위치 설정
        if (preHero.dir == Direction.UP) pos = new Vector2(pPos.x, pPos.y - offset);
        else if (preHero.dir == Direction.DOWN) pos = new Vector2(pPos.x, pPos.y + offset);
        else if (preHero.dir == Direction.LEFT) pos = new Vector2(pPos.x + offset, pPos.y);
        else if (preHero.dir == Direction.RIGHT) pos = new Vector2(pPos.x - offset, pPos.y);

        GameObject temp = Instantiate(prefabHeroes[id], pos, Quaternion.identity, transform.GetChild(0));   //영웅 생성

        HeroMoveController hero = temp.GetComponent<HeroMoveController>();

        for (int i = 0; i < preHero.GET_POS_DIR.Count; i++)
        {
            hero.AddPosDir(preHero.GET_POS_DIR[i].pos, preHero.GET_POS_DIR[i].dir);
            //앞 영웅의 posDir 리스트의 데이터를 생성한 영웅 리스트에도 추가해줌  
        }

        hero.Move(preHero.dir);         //생성한 영웅 이동
        heroList.Add(hero);             //생성한 영웅 리스트에 추가
    }
}
