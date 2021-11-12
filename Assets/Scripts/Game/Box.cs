using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    [SerializeField] private AudioSource dragSound;
    [SerializeField] private float moveDuration;
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
            //a caixa não está bloqueada, então pode se mover
            StartCoroutine(MoveBox(direction));
            return true;
        }   
    }

    IEnumerator MoveBox(Vector3 d)
    {
        transform.DOMove(transform.position + d, moveDuration);
        dragSound.Play();
        yield return new WaitForSeconds(moveDuration);
        TestForOnMarker();
        yield return null;
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
                marker.sfx.Play();
                return;
            }
        }
        m_OnMarker = false;
    }

}
