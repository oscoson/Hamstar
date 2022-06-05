using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour
{
    public int currentLevel;
    public Animator transition;

    public AudioMixer masterMixer;
    public AudioMixer musicMixer;
    public AudioMixer soundMixer;

    public void Start()
    {
        masterMixer.SetFloat("Master", PlayerPrefs.GetFloat("MasterVolume"));
        musicMixer.SetFloat("Music", PlayerPrefs.GetFloat("MusicVolume"));
        soundMixer.SetFloat("Sounds", PlayerPrefs.GetFloat("SoundVolume"));
    }
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
        yield return new WaitForSeconds(2.75f);
        SceneManager.LoadScene(currentLevel);
    }


}
