using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class SICharacterController : MonoBehaviour
{
    public float fireRate;
    public float followInterval;
    public GameObject bullet;
    public GameObject[] hpIcons;
    public GameObject GameOverText;

    Vector2 mousePos;
    bool followed;
    Transform shootPos;
    int maxhp = 3;
    int hp;

    // Start is called before the first frame update
    void Start()
    {
        hp = 3;
        GameOverText.SetActive(false);
        //hpIcons = new GameObject[maxhp];
        followed = true;
        shootPos = transform.GetChild(0);
        InvokeRepeating("ShootBullets", 3, fireRate);
    }

    // Update is called once per frame
    void Update()
    {
        if (followed)
        {
            StartCoroutine("FollowPlayer");
        }

        if (hp <= 0)
        {
            hp = 0;
            GameOverText.SetActive(true);
        }
        for(int i = 0; i < maxhp; i++)
        {
            //Debug.Log(hpIcons[i].name);
            if (i > (hp - 1))
            {
                hpIcons[i].SetActive(false);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("AlienBullet"))
        {
            Destroy(collision.gameObject);
            hp--;
        }
    }

    IEnumerator FollowPlayer()
    {
        followed = false;
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        yield return new WaitForSeconds(followInterval);
        transform.position = new Vector2(mousePos.x, transform.position.y);
        followed = true;
    }

    void ShootBullets()
    {
        Instantiate(bullet, shootPos.position, shootPos.rotation);
    }
}
