using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrunkController : MonoBehaviour
{
    public Transform door1, door2, door3;

    int currentDoor;

    private void Start()
    {
        currentDoor = 1;
        transform.position = door1.position;
    }
    private void Update()
    {
        switch (currentDoor)
        {
            case 1:
                transform.position = door1.position;
                if (Input.GetKeyDown(KeyCode.W))
                {
                    currentDoor = 2;
                }
                break;
            case 2:
                transform.position = door2.position;
                if (Input.GetKeyDown(KeyCode.W))
                {
                    currentDoor = 3;
                }
                if (Input.GetKeyDown(KeyCode.S))
                {
                    currentDoor = 1;
                }
                break;
            case 3:
                transform.position = door3.position;
                if (Input.GetKeyDown(KeyCode.S))
                {
                    currentDoor = 2;
                }
                break;
        }
            
    }
}
