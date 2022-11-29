using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    public void GoToHorizontal()
    {
        SceneManager.LoadScene("HorizontalLevel");
    }
    public void GoToTTD()
    {
        SceneManager.LoadScene("VerticalLevel - ttd");
    }
    public void GoToDTT()
    {
        SceneManager.LoadScene("VerticalLevel - dtt");
    }
    public void GoToLevelSelect()
    {
        SceneManager.LoadScene("Level Select");
    }
}
