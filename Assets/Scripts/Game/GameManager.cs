using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public LevelBuilder m_LevelBuilder;
    public GameObject m_NextButton;
    public AudioSource completeSound;
    //SCORE_______________________________________
    [SerializeField] private Text placedBoxesText;
    [SerializeField] private Text movementsText;
    [SerializeField] private Text timerText;
    private float startTime;
    //____________________________________________
    [SerializeField] private Player m_Player;
    [SerializeField] private string nextScene;
    private bool audioPlayed;

    void Start()
    {
        m_LevelBuilder.Build();
        m_Player = FindObjectOfType<Player>();
        audioPlayed = false;
        startTime = Time.time;
    }

    void Update()
    {
        Timer();
        m_NextButton.SetActive(IsLevelComplete());
        if (IsLevelComplete() is true && audioPlayed is false)
        {
            completeSound.Play();
            audioPlayed = true;
        }
        m_Player.Movement();
        Box[] boxes = FindObjectsOfType<Box>();
        placedBoxesText.text = placedBoxes().ToString() + "/" + boxes.Length.ToString();
        movementsText.text = "MOVEMENTS: " + m_Player.movements.ToString();
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(nextScene);
    }

    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public bool IsLevelComplete()
    {
        Box[] boxes = FindObjectsOfType<Box>();
        foreach (var box in boxes)
        {
            if (!box.m_OnMarker)
            { 
                return false;
            }
        }
        return true;
    }

    public int placedBoxes()
    {
        int value = 0;
        Box[] boxes = FindObjectsOfType<Box>();
        foreach (var box in boxes)
        {
            if (box.m_OnMarker)
            {
               value++; 
            }
            if (!box.m_OnMarker && value > 0)
            {
               value--;
            }
        }
        return value;
    }

    void Timer()
    {
        float t = Time.time - startTime;
        string minutes = ((int) t / 60).ToString("00");
        string seconds = (t % 60).ToString("00");
        timerText.text = minutes + ":" + seconds;
    }

}
