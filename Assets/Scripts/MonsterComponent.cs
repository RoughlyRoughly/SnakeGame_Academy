using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterComponent : MonoBehaviour
{
    [SerializeField] int hp = 10;
    [SerializeField] int atk = 5;
    [SerializeField] float atkRate = 0.5f;
    bool isAttack = false;

    Slider hpBar;
    SpriteRenderer sr;
    Camera mainCam;
    float hpBarY = 0.85f;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        mainCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (hpBar) hpBar.transform.position = mainCam.WorldToScreenPoint(transform.position + new Vector3(0, hpBarY, 0));
    }

    public void SetHpBar(GameObject _hpBar)
    {
        hpBar = _hpBar.GetComponent<Slider>();
        hpBar.maxValue = hp;
        hpBar.value = hp;
    }
}
