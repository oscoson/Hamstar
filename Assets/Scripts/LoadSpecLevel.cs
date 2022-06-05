using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSpecLevel : MonoBehaviour
{
    public Animator levelTransition;
    public int levelIndex;

    //This script is essentially the same as Main Menu, but contains less
    //public variables than main menu -> looks much cleaner, revise in future

    public void LoadLevel()
    {
        levelTransition.SetTrigger("Start");
        StartCoroutine(waitForTime());
    }


    IEnumerator waitForTime()
    {
        yield return new WaitForSeconds(2.75f);
        SceneManager.LoadScene(levelIndex);
    }
}
