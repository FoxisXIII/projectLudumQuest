using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boton : Interactuable
{
    private bool pressed;

    public AudioSource AudioSource;

    public bool hasToActivate;
    public Interactuable activateThis;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Floor") && !other.CompareTag("Ladder"))
            if (!hasToActivate || hasToActivate && activateThis.On())
            {
                if (!other.isTrigger)
                {
                    AudioSource.Play();
                }
            }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!other.CompareTag("Floor") && !other.CompareTag("Ladder"))
            if (!hasToActivate || hasToActivate && activateThis.On())
            {
                if (!other.isTrigger)
                {
                    pressed = true;
                    transform.GetChild(0).gameObject.SetActive(false);
                    transform.GetChild(1).gameObject.SetActive(true);
                }
            }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Floor") && !other.CompareTag("Ladder"))
            if (!hasToActivate || hasToActivate && activateThis.On())
            {
                if (!other.isTrigger)
                {
                    pressed = false;
                    transform.GetChild(0).gameObject.SetActive(true);
                    transform.GetChild(1).gameObject.SetActive(false);
                }
            }
    }

    public override bool On()
    {
        return pressed;
    }
}