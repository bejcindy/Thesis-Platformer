using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienController : MonoBehaviour
{
    public float fireRate;
    public float energyNeed;
    public GameObject bullet;
    public Sprite[] animSprites;
    public AudioClip rechargeAudio;

    Transform[] shootPos;
    bool enoughEnergy;
    bool canShoot;
    float energy;
    SpriteRenderer rend;
    bool isOver;
    bool noseContact;
    float fillPercentage;
    AudioSource aud;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        aud = GetComponent<AudioSource>();
        shootPos = new Transform[transform.childCount];
        for(int i = 0; i < transform.childCount; i++)
        {
            shootPos[i] = transform.GetChild(i);
        }
        InvokeRepeating("ShootBullets", 0, fireRate);
    }

    // Update is called once per frame
    void Update()
    {
        //rend.color = new Color(rend.color.r, rend.color.g, rend.color.b, Mathf.Lerp(.3f, 1f, Mathf.InverseLerp(0, energyNeed, energy)));
        fillPercentage = Mathf.Lerp(0f, 1f, Mathf.InverseLerp(0, energyNeed, energy));
        if (fillPercentage >= 0 && fillPercentage <= .2f)
        {
            rend.sprite = animSprites[0];
        }
        else if (fillPercentage > .2f && fillPercentage <= .4f)
        {
            rend.sprite = animSprites[1];
        }
        else if (fillPercentage > .4f && fillPercentage <= .6f)
        {
            rend.sprite = animSprites[2];
        }
        else if (fillPercentage > .6f && fillPercentage <= .8f)
        {
            rend.sprite = animSprites[3];
        }
        else if (fillPercentage > .8f && fillPercentage < 1f)
        {
            rend.sprite = animSprites[4];
        }
        else if (fillPercentage == 1f)
        {
            rend.sprite = animSprites[5];
        }
        
        
    }

    private void OnMouseOver()
    {
        isOver = true;
        if (Input.GetMouseButton(0) && noseContact)
        {
            
            if (energy < energyNeed)
            {
                //energy fill up
                energy += Time.deltaTime;
                enoughEnergy = false;
                if (!aud.isPlaying)
                {
                    aud.PlayOneShot(rechargeAudio);
                }
            }
            else
            {
                energy = energyNeed;
                enoughEnergy = true;
                canShoot = true;
            }
            
        }
    }

    private void OnMouseExit()
    {
        aud.Stop();
    }



    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
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

    void ShootBullets()
    {
        if (canShoot)
        {
            for (int i = 0; i < shootPos.Length; i++)
            {
                Instantiate(bullet, shootPos[i].position, shootPos[i].rotation);
            }
            energy = 0;
            enoughEnergy = false;
            canShoot = false;
        }
    }
}
