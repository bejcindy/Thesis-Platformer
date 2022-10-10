using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitController : MonoBehaviour
{
    public GameObject completeText;

    // Start is called before the first frame update
    void Start()
    {
        completeText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            completeText.SetActive(true);
            collision.gameObject.SetActive(false);
        }
    }
}
