using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public GameObject linkedTeleporter;
    public Teleporter linkedTeleporterScript;

    void Update()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (var player in players)
        {
            if (player.transform.position.x == this.transform.position.x && player.transform.position.z == this.transform.position.z)
            {
                linkedTeleporterScript.enabled = false;
                player.transform.position = linkedTeleporter.transform.position;
            }
        }
        GameObject[] boxes = GameObject.FindGameObjectsWithTag("Box");
        foreach(var box in boxes)
        {
            if (box.transform.position.x == this.transform.position.x && box.transform.position.z == this.transform.position.z)
            {
                linkedTeleporterScript.enabled = false;
                box.transform.position = linkedTeleporter.transform.position;
            }
        }
    }
}
