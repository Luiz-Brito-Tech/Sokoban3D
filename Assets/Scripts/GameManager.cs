using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public LevelBuilder m_LevelBuilder;
    public GameObject m_NextButton;
    //private bool m_ReadyForInput;
    private Player m_Player;

    void Start()
    {
        m_LevelBuilder.Build();
        m_Player = FindObjectOfType<Player>();
    }

    void Update()
    {
        m_NextButton.SetActive(IsLevelComplete());
        m_Player.Movement();
    }

    public void NextLevel()
    {
        ClearScene();
        m_NextButton.SetActive(false);
        m_LevelBuilder.NextLevel();
        m_LevelBuilder.Build();
        m_Player = FindObjectOfType<Player>(); 
    }

    public void ResetScene()
    {
        ClearScene();
        m_NextButton.SetActive(false);
        m_LevelBuilder.Build();
        m_Player = FindObjectOfType<Player>();
    }

    public void ClearScene()
    {
        string[] tagsToBeDestroyed = {"Wall", "Box", "Player", "Marker", "Ground"};
        foreach(string tag in tagsToBeDestroyed)
        {
            var objects = GameObject.FindGameObjectsWithTag(tag);
            foreach (GameObject o in objects)
            {
                Destroy(o.gameObject);
            }
        }
    }

    bool IsLevelComplete()
    {
        Box[] boxes = FindObjectsOfType<Box>();
        foreach (var box in boxes)
        {
            if (!box.m_OnMarker) return false;
        }
        return true;
    }


}
