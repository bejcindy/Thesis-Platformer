using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SITrunkController : MonoBehaviour
{
    public AudioClip snotSound;

    AudioSource aud;

    // Start is called before the first frame update
    void Start()
    {
        aud = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (!aud.isPlaying)
            {
                aud.clip = snotSound;
                aud.Play();
            }
        }
        else
        {
            aud.Stop();
        }
    }
}
