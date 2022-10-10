using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefillerController : MonoBehaviour
{
    public float resetTime;

    bool inactive;
    float t;

    // Start is called before the first frame update
    void Start()
    {
        inactive = false;
        t = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (inactive)
        {
            if (t < resetTime)
            {
                t += Time.deltaTime;
            }
            else
            {
                inactive = false;
                t = 0;
                gameObject.GetComponent<Renderer>().enabled = true;
                gameObject.GetComponent<Collider2D>().enabled = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().gasSlider.value = collision.gameObject.GetComponent<PlayerController>().gasSlider.maxValue;
            gameObject.GetComponent<Renderer>().enabled = false;
            gameObject.GetComponent<Collider2D>().enabled = false;
            inactive = true;
        }
    }
}
