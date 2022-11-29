using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeDoorController : MonoBehaviour
{
    public GameObject fakeText;

    public GameObject player;

    float t;
    bool gotPlayer;

    // Start is called before the first frame update
    void Start()
    {
        fakeText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (gotPlayer)
        {
            t += Time.deltaTime;
        }
        if (t > 3)
        {
            fakeText.SetActive(false);
            player.SetActive(true);
            player.GetComponent<PlayerController>().respawn = true;
            t = 0;
            gotPlayer = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            fakeText.SetActive(true);
            collision.gameObject.SetActive(false);
            //player = collision.gameObject;
            gotPlayer = true;
        }
    }
}
