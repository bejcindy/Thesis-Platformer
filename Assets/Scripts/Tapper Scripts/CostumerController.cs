using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CostumerController : MonoBehaviour
{
    public Sprite[] differentCostumers;
    public float normalSpeed, angrySpeed;
    public int doorNumber;

    float rightSideLimit;
    SpriteRenderer sr;
    bool angry;
    bool happy;
    bool added;
    bool minused;
    bool canDrink;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = differentCostumers[Random.Range(0, differentCostumers.Length)];
        angry = false;
        rightSideLimit = 5 - doorNumber;
        TapperGM.costumerNumber++;
        canDrink=true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!happy)
        {
            if (transform.position.x < rightSideLimit)
            {
                if (!angry)
                {
                    transform.position += new Vector3(normalSpeed, 0, 0);
                }
                else
                {
                    transform.position += new Vector3(angrySpeed, 0, 0);
                }
            }
        }
        else
        {
            transform.position -= new Vector3(angrySpeed, 0, 0);
            if (!minused)
            {
                TapperGM.life--;
                minused = true;
            }
            
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Beer")&&canDrink)
        {
            happy = true;
            Destroy(collision.gameObject);
            canDrink=false;
        }
        if (collision.CompareTag("Snot")&&canDrink)
        {
            angry = true;
            Destroy(collision.gameObject);
            canDrink=false;
        }
    }
}
