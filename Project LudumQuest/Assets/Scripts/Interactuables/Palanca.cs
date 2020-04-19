using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Palanca : IInteractuable
{
    private bool on;

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