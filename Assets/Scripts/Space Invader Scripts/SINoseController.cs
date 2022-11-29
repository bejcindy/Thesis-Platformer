using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SINoseController : MonoBehaviour
{
    public static bool goBack;
    public GameObject noseMiddle;
    public GameObject noseBottom;

    Vector2 lastPos;
    Vector2 mousePos;
    GameObject[] noseMiddles;

    // Start is called before the first frame update
    void Start()
    {
        lastPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        if (goBack)
        {
            //Cursor.lockState = CursorLockMode.Locked;
            //InputState.Change(mousePos, warpPosition);
            //Cursor.visible = false;
            for(int i = 0; i < noseMiddles.Length; i++)
            {
                Destroy(noseMiddles[i]);
            }
            transform.position = noseBottom.transform.position;
            lastPos = noseBottom.transform.position;
            
        }
        else
        {
            //Cursor.lockState = CursorLockMode.None;
            //Cursor.visible = true;
            transform.position = mousePos;
            if (Vector2.Distance(lastPos, mousePos) > .5f)
            {
                Instantiate(noseMiddle, transform.position, Quaternion.FromToRotation(lastPos, mousePos));
                noseMiddles = GameObject.FindGameObjectsWithTag("NoseMiddle");
                lastPos = mousePos;
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            goBack = true;
        }
    }
    //private void OnMouseOver()
    //{
    //    if (goBack)
    //    {
    //        goBack = false;
    //    }
    //}
}
