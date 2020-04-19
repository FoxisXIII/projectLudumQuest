using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource Music, UI;
    private void Start()
    {
        DontDestroyOnLoad (gameObject);
    }

    public void PlayUI(AudioClip clip)
    {
        UI.clip = clip;
        UI.Play();
    }
    public void PlayMusic(AudioClip clip)
    {
        Music.clip = clip;
        Music.Play();
    }
}