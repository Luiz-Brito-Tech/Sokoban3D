using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{

    private AudioSource sound;
    bool musicON = true;

    void Start()
    {
        sound = GetComponent<AudioSource>();
    }

    public void PlaySound()
    {
        sound.Play();
    }

    public void MusicSwitch()
    {
        if (musicON is true)
        {
            sound.Pause();
            musicON = false;
        }
        else
        {
            sound.Play();
            musicON = true;
        }
    }

}
