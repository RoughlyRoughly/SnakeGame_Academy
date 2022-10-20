using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Transform headHero;

    // Start is called before the first frame update

    //public static CameraManager i;

    //void Start() // ����!
    //{
    //    i = this;
    //}

    //void Awake() // Start ���� �ٸ� ��ũ��Ʈ���� �� ��ũ��Ʈ�� Instance�� ����� �� �����Ƿ� Awake���� �޾���
    //{
    //    i = this; // �ٵ� �׷� �ٸ� ��ũ��Ʈ���� Awake���� �� ��ũ��Ʈ�� Instance�� ����� �� Awake���� �޾��ִ°͵� ������ �� �ִ�.
    //}

    // �׷� ���ɾ�����

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
                    Debug.LogError("CameraManager�� �ν��Ͻ��� ����!");
                }
            }

            return instance;
        }
    }

    // �̷��� ����.
    // proterty��� C#�� ����� ���, Instance�� �ٸ� ��ũ��Ʈ���� ȣ���� �� get�κ��� �Լ��� �����
    // �� get���� instance�� ������ null�̸� CameraManager�� �ϳ� ã�ƿͼ� instance�� �־��� �� return �� ��ȯ���ִ� ����� ��.
    // �ٵ� �� ��� ���� ������� �� �ʿ� ����, �׳� ���� ���ٰ�

    public void SetHeadHero(Transform hero)
    {
        headHero = hero;
    }

    private void LateUpdate()
    {
        if (headHero != null) transform.position = headHero.position;
    }
}
