using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public bool m_OnMarker;//retorna verdadeiro se a caixa estiver em cima do marcador
    public string color;

    public bool Move(Vector3 direction) //bloqueia o movimento diagonal
    {
        if (BoxBlocked(transform.position, direction))
        {
            return false;    
        }
        else
        {
            transform.Translate(direction); //a caixa não está bloqueada, então pode se mover
            TestForOnMarker();
            return true;
        }   
    }

    bool BoxBlocked(Vector3 position, Vector3 direction)
    {
        Vector3 newPos = new Vector3(position.x, 0, position.z) + direction;
        GameObject[] walls = GameObject.FindGameObjectsWithTag("Wall");
        foreach (var wall in walls)
        {
            if (wall.transform.position.x == newPos.x && wall.transform.position.z == newPos.z)
            {
                return true;
            }
        }
        GameObject[] boxes = GameObject.FindGameObjectsWithTag("Box");
        foreach(var box in boxes)
        {
            if (box.transform.position.x == newPos.x && box.transform.position.z == newPos.z)
            {
                return true;
            }
        }
        return false;
    }

    void TestForOnMarker()
    {
        Marker[] markers = FindObjectsOfType<Marker>();
        foreach(var marker in markers)
        {
            if (transform.position.x == marker.transform.position.x && transform.position.z == marker.transform.position.z
            && color == marker.color)
            {//está no marcador
                m_OnMarker = true;
                return;
            }
        }
        m_OnMarker = false;
    }

}
