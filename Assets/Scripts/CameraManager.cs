using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Transform headHero;

    // Start is called before the first frame update

    //public static CameraManager i;

    //void Start() // 멈춰!
    //{
    //    i = this;
    //}

    //void Awake() // Start 에선 다른 스크립트들이 이 스크립트이 Instance를 사용할 수 있으므로 Awake에서 받아줌
    //{
    //    i = this; // 근데 그럼 다른 스크립트들의 Awake에서 이 스크립트의 Instance를 사용할 땐 Awake에서 받아주는것도 문제일 수 있다.
    //}

    // 그럼 어케쓰느냐

    private static CameraManager instance = null;
    public static CameraManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<CameraManager>();

                if (instance == null)
                {
                    Debug.LogError("CameraManager의 인스턴스가 없음!");
                }
            }

            return instance;
        }
    }

    // 이렇게 쓴다.
    // proterty라는 C#의 기능을 사용, Instance를 다른 스크립트에서 호출할 때 get부분의 함수가 실행됨
    // 그 get에서 instance의 변수가 null이면 CameraManager를 하나 찾아와서 instance에 넣어준 후 return 즉 반환해주는 기능을 함.
    // 근데 넌 사실 아직 여기까지 알 필욘 없어, 그냥 내가 쓴다고

    public void SetHeadHero(Transform hero)
    {
        headHero = hero;
    }

    private void LateUpdate()
    {
        if (headHero != null) transform.position = headHero.position;
    }
}
