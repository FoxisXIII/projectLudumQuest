using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ink : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && Input.GetKey(KeyCode.E))
        {
            other.GetComponent<PlayerController>().inkSpeed =Mathf.Max(other.GetComponent<PlayerController>().inkSpeed - 0.0025f,0.0025f);
            GameController.getInstance().LevelManager.audioSource.clip=GetComponent<AudioSource>().clip;
            GameController.getInstance().LevelManager.audioSource.Play();
            if (other.GetComponent<PlayerController>().ink > 1)
            {
                other.GetComponent<PlayerController>().ink = 1;
            }

            Destroy(gameObject);
        }
    }
}