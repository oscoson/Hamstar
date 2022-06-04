using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public int currentLevel;
    public void PlayButton()
    {
        Debug.Log("yowkeoy");
        SceneManager.LoadScene(currentLevel);
    }
    

    public void QuitButton()
    {
        Application.Quit();
    }


}
