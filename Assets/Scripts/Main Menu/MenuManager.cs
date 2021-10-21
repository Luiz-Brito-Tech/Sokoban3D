using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void PlayButton()
    {
        SceneManager.LoadScene(1);
    }

    public void LevelsButton()
    {
        print("Opens the menu that displays the unlocked levels");
    }

    public void ExitButton()
    {
        Application.Quit();
    }

}
