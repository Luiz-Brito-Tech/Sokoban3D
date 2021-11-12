using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marker : MonoBehaviour
{
    public string color;
    public AudioSource sfx;

    void Start()
    {
        sfx = GetComponent<AudioSource>();
    }
}
