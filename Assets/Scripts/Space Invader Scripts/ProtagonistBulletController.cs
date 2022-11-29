using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtagonistBulletController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Nose")|| collision.CompareTag("NoseMiddle"))
        {
            SINoseController.goBack = true;
            Destroy(gameObject);
        }
    }
}
