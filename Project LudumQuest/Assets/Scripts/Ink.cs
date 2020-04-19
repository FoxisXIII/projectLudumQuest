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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().inkSpeed =
                Mathf.Max(other.GetComponent<PlayerController>().inkSpeed - 0.0025f, 0.0025f);
            GameController.getInstance().LevelManager.audioSource.clip = GetComponent<AudioSource>().clip;
            GameController.getInstance().LevelManager.audioSource.Play();
            if (other.GetComponent<PlayerController>().ink > 1)
            {
                other.GetComponent<PlayerController>().ink = 1;
            }

            InvokeRepeating("Dissolve", 0, Time.deltaTime);
        }
    }

    public void Dissolve()
    {
        var color = GetComponent<SpriteRenderer>().color;
        if (color.a <= 0)
            Destroy(gameObject);
        color = new Color(color.r,
            color.g, color.b,
            color.a - Time.deltaTime);
        GetComponent<SpriteRenderer>().color = color;
    }
}