using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void PlayButton()
    {
        StartCoroutine(WaitForSoundAndStart());
    }

    IEnumerator WaitForSoundAndStart()
    {
        yield return new WaitForSeconds(.2f);
        SceneManager.LoadScene(1);
    }

    public void ExitButton()
    {
        Application.Quit();
    }

}
