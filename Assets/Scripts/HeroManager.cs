using System.Collections;
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
        headHero.SetHead();             //현재 영웅을 머리로 세팅
        CameraManager.i.SetHeadHero(headHero.transform);            //카메라에 헤드 히어로 연결
        heroList.Add(headHero);         //리스트에 헤드, 히어로 추가
        headHero.Move(Direction.UP);    //영웅 이동
    }

    void ChangeDirection(Direction _dir)
    {
        headHero.Move(_dir);        //머리영웅 방향변경

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
        else if(Input.GetKeyDown(KeyCode.D))
        {
            if (headHero.dir == Direction.RIGHT || headHero.dir == Direction.LEFT) return;

            ChangeDirection(Direction.RIGHT);
        }
    }
}
