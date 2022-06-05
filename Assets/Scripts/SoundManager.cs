using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public AudioMixer masterMixer;
    public AudioMixer musicMixer;
    public AudioMixer soundMixer;

    public Slider masterVolumeSlider;
    public Slider musicVolumeSlider;
    public Slider soundVolumeSlider;


    void Start()
    {
        Debug.Log(PlayerPrefs.GetInt("HasChanged", 0));
        if(PlayerPrefs.GetInt("HasChanged", 0) == 0)
        {
            Debug.Log("Change!");
            LoadValues();
        }
    }

    public void SetMaster (float volume)
    {
        musicMixer.SetFloat("Master", volume);
        PlayerPrefs.SetFloat("MasterVolume", volume);
        masterVolumeSlider.value = volume;
        PlayerPrefs.SetInt("HasChanged", 0);
    }
    public void SetMusic (float volume)
    {
        musicMixer.SetFloat("Music", volume);
        PlayerPrefs.SetFloat("MusicVolume", volume);
        musicVolumeSlider.value = volume;
        PlayerPrefs.SetInt("HasChanged", 0);
        Debug.Log(PlayerPrefs.GetInt("HasChanged", 1245));
    }

    public void SetSound (float volume)
    {
        musicMixer.SetFloat("Sounds", volume);
        PlayerPrefs.SetFloat("SoundVolume", volume);
        soundVolumeSlider.value = volume;
        PlayerPrefs.SetInt("HasChanged", 0);
    }

    void LoadValues()
    {
        SetMaster(PlayerPrefs.GetFloat("MasterVolume"));
        SetMusic(PlayerPrefs.GetFloat("MusicVolume"));
        SetSound(PlayerPrefs.GetFloat("SoundVolume"));
    }
}
