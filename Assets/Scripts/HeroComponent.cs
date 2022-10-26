using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum HERO
{
    KNIGHT
}

public class HeroComponent : MonoBehaviour
{
    [SerializeField] protected HERO heroId = 0;  //영웅 타입(직업)
    [SerializeField] int hp = 50;
    [SerializeField] protected int atk = 5;
    [SerializeField] float atkRate = 0.5f;

    protected bool isAttack = false;

    [SerializeField] protected GameObject weapon;
    float hpBarY = 0.85f;

    Slider hpBar;
    SpriteRenderer sr;

    Camera mainCam;

    // Start is called before the first frame update
    void Start()
    {
        sr = transform.parent.GetComponentInChildren<SpriteRenderer>();
        mainCam = Camera.main;
    }

    private void FixedUpdate()
    {
        if (hpBar) hpBar.transform.position = mainCam.WorldToScreenPoint(transform.position + new Vector3(0, hpBarY, 0));
    }

    public void HeroInit(GameObject _bar)
    {
        hpBar = _bar.GetComponent<Slider>();
        hpBar.maxValue = hp;
        hpBar.value = hp; 
    }

}
