using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SIGameManager : MonoBehaviour
{
    public GameObject loseText;

    // Start is called before the first frame update
    void Start()
    {
        loseText.SetActive(false);
        Time.timeScale=1;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            loseText.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //goto level selection scene
            SceneManager.LoadScene("Level Select");
        }
    }
}
