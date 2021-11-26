using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    [SerializeField] private AudioSource dragSound;
    [SerializeField] private AudioSource hitSound;
    [SerializeField] private ParticleSystem dust;
    [SerializeField] private float moveDuration;
    public bool m_OnMarker;//retorna verdadeiro se a caixa estiver em cima do marcador
    public string color;

    void Update()
    {
        dust.transform.position = transform.position * .75f;
    }

    public bool Move(Vector3 direction) //bloqueia o movimento diagonal
    {
        if(BlockAhead(transform.position, direction))
        {
            hitSound.Play();
        }
        if (BoxBlocked(transform.position, direction))
        {
            hitSound.Play();
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
        ParticleSystem dustOBJ = Instantiate(dust, new Vector3(transform.position.x, 0, transform.position.z), Quaternion.identity);
        transform.DOMove(transform.position + d, moveDuration);
        dragSound.Play();
        dust.Play();
        yield return new WaitForSeconds(moveDuration);
        TestForOnMarker();
        Destroy(dustOBJ.gameObject);
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
                marker.m_OnBox = true;
                marker.sfx.Play();
                return;
            }
            else
            {
                marker.m_OnBox = false;
            }
        }
        m_OnMarker = false;
    }

    bool BlockAhead(Vector3 position, Vector3 direction)
    {
        Vector3 newPos = new Vector3(position.x, 0, position.z) + (2 * direction);
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

}
