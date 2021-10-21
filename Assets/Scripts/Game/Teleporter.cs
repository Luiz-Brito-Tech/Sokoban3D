using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public GameObject linkedTeleporter;

    void Update()
    {
        string[] tagsToBeFound = {"Box", "Player"};
        foreach(string tag in tagsToBeFound)
        {
            var objects = GameObject.FindGameObjectsWithTag(tag);
            foreach (GameObject o in objects)
            {
                if (o.transform.position.x == this.transform.position.x && o.transform.position.z == this.transform.position.z)
                {
                    o.transform.position = linkedTeleporter.transform.position;
                }
                else if(o.transform.position.x == linkedTeleporter.transform.position.x && o.transform.position.z == linkedTeleporter.transform.position.z)
                {
                    o.transform.position = this.transform.position;
                }
            }
        }
    }
}
