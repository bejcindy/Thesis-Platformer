using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMover : MonoBehaviour
{
    public float moveStep;
    public float moveRate;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Move", 0, moveRate);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Move()
    {
        Vector2 moveDist = new Vector2(0, moveStep);
        transform.position += (transform.rotation * moveDist);
    }
}
