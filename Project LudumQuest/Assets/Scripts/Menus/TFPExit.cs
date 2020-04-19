using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TFPExit : MonoBehaviour
{
    private float time;
    void Update()
    {
        time += Time.deltaTime;

        if (time >= 3)
        {
            Application.Quit();
        }
    }
}
