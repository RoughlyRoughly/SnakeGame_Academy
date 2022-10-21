using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHero : MonoBehaviour
{
    [SerializeField] int id = 0;        //���� �ε���
    
    // Start is called before the first frame update
    void Start()
    {
        
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Hero"))
        {
            HeroManager.i.AddHero(id);
            Destroy(gameObject);
        }
    }
}
