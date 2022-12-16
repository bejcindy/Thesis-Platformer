using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TapperGM : MonoBehaviour
{
    public static int life;
    public static int costumerNumber;
    public GameObject loseText, winText;
    public int MaxCostumer;

    // Start is called before the first frame update
    void Start()
    {
        life = 3;
        loseText.SetActive(false);
        winText.SetActive(false);
        Time.timeScale=1;
    }

    // Update is called once per frame
    void Update()
    {
        if (life == 0)
        {
            loseText.SetActive(true);
            Time.timeScale = 0;
        }
        if (costumerNumber == MaxCostumer)
        {
            winText.SetActive(true);
            Time.timeScale = 0;
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
