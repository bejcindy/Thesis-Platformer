using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnotMaster : MonoBehaviour
{
    public bool runnyNose;
    public GameObject snot;
    public float spawnRange, snotAmount, snotRange;
    public float shootForce;


    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("RunnyNose", 0, 1/snotAmount);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void RunnyNose()
    {
        if (Input.GetButton("Fire1") && runnyNose)
        {
            int burstAmount = Random.Range(2, 4);
            for (int i = 0; i < burstAmount; i++)
            {
                Vector2 randomPos = new Vector2(Random.Range(-spawnRange, spawnRange), 0);
                Vector2 spawnPos = (Vector2)transform.position + randomPos;

                GameObject oneSnot = Instantiate(snot, spawnPos, Quaternion.identity);

                Vector2 direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
                //Vector2 randomDir = direction * Quaternion.Euler(0, 0, Random.Range(-snotRange, snotRange));
                Vector2 randomDir = Quaternion.AngleAxis(Random.Range(-snotRange, snotRange), Vector3.forward) * direction;
                oneSnot.GetComponent<Rigidbody2D>().AddForce(randomDir * shootForce);
            }
        }
    }
}
