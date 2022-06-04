using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public int currentLevel;
    public Animator transition;
    public void PlayButton()
    {
        transition.SetTrigger("Start");
        StartCoroutine(waitForTime());
    }
    

    public void QuitButton()
    {
        Application.Quit();
    }

    IEnumerator waitForTime()
    {
        yield return new WaitForSeconds(1.75f);
        SceneManager.LoadScene(currentLevel);
    }


}
