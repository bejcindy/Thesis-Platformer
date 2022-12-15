using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonkerController : MonoBehaviour
{
    public Sprite[] bonkers;

    SpriteRenderer sr;
    int hp = 8;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hp >= 0)
        {
            sr.sprite = bonkers[8 - hp];
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("AlienBullet"))
        {
            Debug.Log("hit");
            Destroy(collision.gameObject);
            hp--;
        }
        if (collision.CompareTag("Bullet"))
        {
            Debug.Log("down");
            Destroy(collision.gameObject);
        }
        Debug.Log(collision.gameObject.name);
    }
}
