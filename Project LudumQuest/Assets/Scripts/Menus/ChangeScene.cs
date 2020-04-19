using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public string level;

    public AudioClip Clip;
    

    public void LoadScene()
    {
        FindObjectOfType<AudioManager>().PlayUI(Clip);
        SceneManager.LoadScene(level);
    }

    public void Exit()
    {
        FindObjectOfType<AudioManager>().PlayUI(Clip);
        SceneManager.LoadScene("TFP");
    }

    public void Retry()
    {
        FindObjectOfType<AudioManager>().PlayUI(Clip);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}