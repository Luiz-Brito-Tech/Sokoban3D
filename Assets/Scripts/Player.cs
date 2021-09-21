using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool Move(Vector3 direction) //Bloqueia o movimento diagonal
    {
        if (Mathf.Abs(direction.x) < 0.5f) //Vai definir que uma das coordenadas sempre será 0
        {
            direction.x = 0;
        }
        else
        {
            direction.z = 0;
        }
        direction.Normalize(); //dá ao vetor de movimento x ou y um valor sempre de magnitude 1
        if(Blocked(transform.position, direction))
        {
            return false;
        }
        else
        {
            transform.Translate(direction);
            return true;
        }
    }

    bool Blocked(Vector3 position, Vector3 direction)
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
        foreach (var box in boxes)
        {
            if (box.transform.position.x == newPos.x && box.transform.position.z == newPos.z)
            {
                Box bx = box.GetComponent<Box>();
                if (bx && bx.Move(direction))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        return false;
    }

}
