using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
    private int HitCount = 0;
    private int MaxHit = 4;
    private Color NormalColor = Color.white;
    
    void Start()
    {

    }


    private void Reposition() 
    {
        transform.position = new Vector3(transform.position.x+Random.Range(-15, 15),
                                        transform.position.y+Random.Range(-15, 15),
                                        0f);
        GetComponent<SpriteRenderer>().color = NormalColor;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Egg(Clone)")
        {
            HitCount++;
            GetComponent<SpriteRenderer>().color = NormalColor * (float)(MaxHit - HitCount) / MaxHit;
            if (HitCount >= MaxHit)
            {
                HitCount = 0;
                Reposition();
            }
        }
    }
}
