using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager i;
    public Transform headHero;
    // Start is called before the first frame update
    void Start()
    {
        i = this;
    }

    public void SetHeadHero(Transform hero)
    {
        headHero = hero;
    }

    private void LateUpdate()
    {
        if (headHero != null) transform.position = headHero.position;
    }
}
