using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SIGameManager : MonoBehaviour
{
    public GameObject loseText;

    // Start is called before the first frame update
    void Start()
    {
        loseText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            loseText.SetActive(true);
        }
    }
}
