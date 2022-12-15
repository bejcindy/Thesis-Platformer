using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeerController : MonoBehaviour
{
    public float moveSpeed;
    public Sprite[] beerSprites, snotSprites;
    public float speed;

    bool isBeer;
    bool noseContact;
    SpriteRenderer sr;
    float liquid;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        liquid = 1;
        isBeer = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position -= new Vector3(moveSpeed, 0, 0);
        //Debug.Log(liquid);
        switch (isBeer)
        {
            case true:
                gameObject.tag = "Beer";
                if (liquid == 1)
                {
                    sr.sprite = beerSprites[0];
                }
                if (liquid < 1 && liquid >= .8f)
                {
                    sr.sprite = beerSprites[1];
                }
                if (liquid < .8f && liquid >= .6f)
                {
                    sr.sprite = beerSprites[2];
                }
                if (liquid < .6f && liquid >= .4f)
                {
                    sr.sprite = beerSprites[3];
                }
                if (liquid < .4 && liquid >= .2f)
                {
                    sr.sprite = beerSprites[4];
                }
                if (liquid < .2 && liquid > 0)
                {
                    sr.sprite = beerSprites[5];
                }
                if (liquid == 0)
                {
                    sr.sprite = beerSprites[6];
                }
                break;
            case false:
                gameObject.tag = "Snot";
                if (liquid == 1)
                {
                    sr.sprite = snotSprites[7];
                }
                if (liquid < 1 && liquid >= 5f/6f)
                {
                    sr.sprite = snotSprites[6];
                }
                if (liquid < 5f/6f && liquid >= 4f/6f)
                {
                    sr.sprite = snotSprites[5];
                }
                if (liquid < 4f/6f && liquid >= 3f/6f)
                {
                    sr.sprite = snotSprites[4];
                }
                if (liquid < 3f/6f && liquid >= 2f/6f)
                {
                    sr.sprite = snotSprites[3];
                }
                if (liquid < 2f/6f && liquid >= 1f/6f)
                {
                    sr.sprite = snotSprites[2];
                }
                if (liquid < 1f/6f && liquid > 0)
                {
                    sr.sprite = snotSprites[1];
                }
                if (liquid == 0)
                {
                    sr.sprite = snotSprites[0];
                }
                break;
        }
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButton(1) && noseContact)
        {
            if (liquid > 0)
            {
                liquid -= Time.deltaTime * speed;
            }
            else
            {
                liquid = 0;
                isBeer = false;
            }
        }
        if (Input.GetMouseButton(0) && noseContact && !isBeer)
        {
            if (liquid < 1)
            {
                liquid += Time.deltaTime * speed;
            }
            else
            {
                liquid = 1;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Nose"))
        {
            noseContact = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Nose"))
        {
            noseContact = false;
        }
    }
}
