using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleSnotController : MonoBehaviour
{
    public float disappearRate;

    SpriteRenderer sr;
    float waitTime = 0.05f;
    float t;
    CircleCollider2D c;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        c = GetComponent<CircleCollider2D>();
        c.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (t < waitTime)
        {
            t += Time.deltaTime;
        }
        else
        {
            c.enabled = true;
        }
        sr.color -= new Color(0, 0, 0, disappearRate);
        c.radius -= (disappearRate * .1f);
        if (sr.color.a <= 0)
        {
            Destroy(gameObject);
        }
    }

   
}
