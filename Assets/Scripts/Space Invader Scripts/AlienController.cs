using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienController : MonoBehaviour
{
    public float fireRate;
    public float energyNeed;
    public GameObject bullet;

    Transform[] shootPos;
    bool enoughEnergy;
    bool canShoot;
    float energy;
    SpriteRenderer rend;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
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
        rend.color = new Color(rend.color.r, rend.color.g, rend.color.b, Mathf.Lerp(.3f, 1f, Mathf.InverseLerp(0, energyNeed, energy)));
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButton(0))
        {
            if (energy < energyNeed)
            {
                //energy fill up
                energy += Time.deltaTime;
                enoughEnergy = false;
            }
            else
            {
                energy = energyNeed;
                enoughEnergy = true;
            }
        }
        if (Input.GetMouseButtonDown(0) && enoughEnergy)
        {
            canShoot = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
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
