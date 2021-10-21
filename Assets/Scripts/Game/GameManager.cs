using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public LevelBuilder m_LevelBuilder;
    public GameObject m_NextButton;
    [SerializeField] private Player m_Player;
    [SerializeField] private string nextScene;

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
        SceneManager.LoadScene(nextScene);
    }

    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
