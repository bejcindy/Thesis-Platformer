using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BartenderController : MonoBehaviour
{
    public GameObject Beer;
    public Transform[] beerPoses;
    public Transform[] doors;
    public float serveRate;


    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Serve", 2, serveRate);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Serve()
    {
        int n = CostumerSpawner.closestCostumer.GetComponent<CostumerController>().doorNumber;
        transform.position = doors[n - 1].position;
        Instantiate(Beer, beerPoses[n - 1].position, Quaternion.identity);
    }
}
