using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    /// <summary>
    /// Maybe all this has to be on the levelController and not there, but I will start coding and if it fits, it fits xD
    /// </summary>
    private float levelLife = 100f;

    [SerializeField] private float speedDestruction = 0.01f;

    private bool destroying = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (destroying)
        {
            destroyLevel();
        }
    }

    //Maybe a levelClass method
    void destroyLevel()
    {
        levelLife -= speedDestruction;
    }
}
