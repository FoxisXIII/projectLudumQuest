using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Palanca : Interactuable
{
    private bool on;

    public AudioSource AudioSource;
    public bool hasToActivate;
    public Interactuable activateThis;

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Floor") && !other.CompareTag("Ladder"))
            if (!hasToActivate || hasToActivate && activateThis.On())
                if (!other.isTrigger && other.CompareTag("Player"))
                {
                    AudioSource.Play();
                    on = !on;
                    transform.GetChild(0).gameObject.SetActive(on);
                    transform.GetChild(1).gameObject.SetActive(!on);
                }
    }

    public override bool On()
    {
        return on;
    }
}