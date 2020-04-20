using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource Music, UI;
    public int target = 60;
    private void Start()
    {
        DontDestroyOnLoad (gameObject);
    }

    void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = target;
    }
     
    void Update()
    {
        if(Application.targetFrameRate != target)
            Application.targetFrameRate = target;
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