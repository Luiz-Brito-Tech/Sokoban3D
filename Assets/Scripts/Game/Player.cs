using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveDuration;
    [SerializeField]private bool m_ReadyForInput;
    Vector3 moveInput;
    //SCORE
    public float movements;

    public void Movement()
    {
        if (m_ReadyForInput)
        {
            moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
            moveInput.Normalize();
            ApplyRotation();
        }
        if (moveInput.sqrMagnitude > 0.5f) //tecla pressionada ou segurada
        {
            if(m_ReadyForInput)
            {
                m_ReadyForInput = false;
                Move(moveInput);
            }
        } 
        else
        {
            m_ReadyForInput = true;
        }
    }

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
            m_ReadyForInput = true;
            return false;
        }
        else
        {
            StartCoroutine(MovePlayer(direction));
            return true;
        }
    }

    IEnumerator MovePlayer(Vector3 direction)
    {
            movements++;
            transform.DOMove(transform.position + direction, moveDuration);
            yield return new WaitForSeconds(moveDuration);
            m_ReadyForInput = true;
            //yield return null;
    }

    void ApplyRotation()
    {
        var rotationVector = transform.rotation.eulerAngles;

        switch(moveInput.x)
        {
            case 1:
                rotationVector.y = 90;
                break;
            case -1:
                rotationVector.y = -90;
                break;
        }
        switch(moveInput.z)
        {
            case 1:
                rotationVector.y = 0;
                break;
            case -1:
                rotationVector.y = 180;
                break;
        }

        transform.rotation = Quaternion.Euler(rotationVector);
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
