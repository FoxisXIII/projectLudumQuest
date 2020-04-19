using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject pause;
    public GameObject victory;
    public GameObject defeat;
    public AudioSource audioSource;
    
    public AudioClip pauseClip;
    public AudioClip restartClip;
    public AudioClip victoryClip;
    public AudioClip defeatClip;

    private void Awake()
    {
        GameController.getInstance().LevelManager = this;
    }

    public void PauseGame()
    {
        if(!(victory.activeInHierarchy || defeat.activeInHierarchy))
        {
            FindObjectOfType<AudioManager>().PlayUI(pauseClip);
            pause.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void RestartGame()
    {
        FindObjectOfType<AudioManager>().PlayUI(restartClip);
        pause.SetActive(false);
        Time.timeScale = 1;
    }

    public void Victory()
    {
        FindObjectOfType<AudioManager>().PlayUI(victoryClip);
        pause.SetActive(false);
        Time.timeScale = 0;
        victory.SetActive(true);
    }

    public void Defeat()
    {
        FindObjectOfType<AudioManager>().PlayUI(defeatClip);
        pause.SetActive(false);
        defeat.SetActive(true);
    }
}