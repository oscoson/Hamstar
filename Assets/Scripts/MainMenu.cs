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
        StartCoroutine(waitForTime());
    }
    

    public void QuitButton()
    {
        Application.Quit();
    }

    IEnumerator waitForTime()
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(currentLevel);
    }


}
