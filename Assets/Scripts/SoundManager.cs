using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public AudioMixer masterMixer;
    public AudioMixer musicMixer;
    public AudioMixer soundMixer;

    public void SetMusic (float volume)
    {
        musicMixer.SetFloat("Music", volume);
    }

    public void SetMaster (float volume)
    {
        musicMixer.SetFloat("Master", volume);
    }

    public void SetSound (float volume)
    {
        musicMixer.SetFloat("Sounds", volume);
    }
}
