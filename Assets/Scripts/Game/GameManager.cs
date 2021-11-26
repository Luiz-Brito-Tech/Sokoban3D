using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public LevelBuilder m_LevelBuilder;
    public GameObject m_NextButton;
    public GameObject pauseMenu;
    public AudioSource completeSound;
    public AudioSource music;
    public AudioSource clickSound;
    bool GameIsPaused = false;
    //SCORE_______________________________________
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
        movementsText.text = "MOVEMENTS: " + m_Player.movements.ToString();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused is true)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void NextLevel()
    {
        StartCoroutine(WaitForSoundAndPass(nextScene));
    }


    public void ResetScene()
    {
        StartCoroutine(WaitForSoundAndPass(SceneManager.GetActiveScene().name));
    }

    IEnumerator WaitForSoundAndPass(string scene)
    {
        if (Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
        }
        yield return new WaitForSeconds(.2f);
        SceneManager.LoadScene(scene);
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

    public void Resume()
    {
        StartCoroutine(WaitForSoundAndResume());
    }

    void Pause()
    {
        StartCoroutine(WaitForSoundAndPause());
    }

    public void GoToMenu()
    {
        StartCoroutine(WaitForSoundAndPass("Main Menu"));
    }

    public void ExitGame()
    {
        StartCoroutine(WaitForSoundAndExit());
    }

    IEnumerator WaitForSoundAndPause()
    {
        pauseMenu.SetActive(true);
        clickSound.Play();
        yield return new WaitForSeconds(.2f);
        GameIsPaused = true;
        Time.timeScale = 0f;
    }

    IEnumerator WaitForSoundAndResume()
    {
        Time.timeScale = 1f;
        clickSound.Play();
        yield return new WaitForSeconds(.2f);
        GameIsPaused = false;
        pauseMenu.SetActive(false);
    }

    IEnumerator WaitForSoundAndExit()
    {
        Time.timeScale = 1f;
        yield return new WaitForSeconds(.2f);
        Application.Quit();
    }

}
