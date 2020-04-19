﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boton : IInteractuable
{
    private bool pressed;

    public AudioSource AudioSource;
    
    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.isTrigger)
        {
            AudioSource.Play();
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        pressed = true;
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(true);
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        pressed = false;
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(1).gameObject.SetActive(false);
    }

    public override bool On()
    {
        return pressed;
    }
}
