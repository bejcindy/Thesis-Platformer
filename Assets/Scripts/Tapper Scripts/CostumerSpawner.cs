using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CostumerSpawner : MonoBehaviour
{
    public Transform[] doors;
    public GameObject costumerPrefab;
    public int spawnCoolDown;
    public static GameObject closestCostumer;

    //float furthestX;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("GenerateCostumer", 0, spawnCoolDown);
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] costumers = GameObject.FindGameObjectsWithTag("Costumer");
        if (costumers.Length == 1)
        {
            closestCostumer = costumers[0];
        }
        else
        {
            for (int i = 0; i < costumers.Length; i++)
            {
                if (i > 0)
                {
                    if (costumers[i - 1].transform.position.x < costumers[i].transform.position.x)
                    {
                        closestCostumer = costumers[i];
                    }
                    else
                    {
                        closestCostumer = costumers[i - 1];
                    }
                }

            }
        }
    }

    void GenerateCostumer()
    {
        int doorN = Random.Range(0, doors.Length);
        GameObject onePerson = Instantiate(costumerPrefab, doors[doorN].position, Quaternion.identity);
        onePerson.GetComponent<CostumerController>().doorNumber = doorN + 1;
    }
}
