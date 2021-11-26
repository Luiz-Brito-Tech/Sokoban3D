using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marker : MonoBehaviour
{
    public string color;
    public AudioSource sfx;
    public bool m_OnBox = false;

    void Start()
    {
        sfx = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (m_OnBox is true)
        {
            gameObject.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
        }
        else
        {
            gameObject.GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
        }
    }
}
