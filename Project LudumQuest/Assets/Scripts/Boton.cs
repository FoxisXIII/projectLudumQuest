using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boton : MonoBehaviour
{
    public Puerta puerta;
    public bool abierta = false, bajando;
    public float speed = 1f;
    public Vector2 posPressed;
    
    // Start is called before the first frame update
    void Start()
    {
        posPressed = new Vector2(transform.position.x, transform.position.y - 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (abierta)
        {
            transform.position=Vector2.MoveTowards(transform.position, posPressed, speed*Time.deltaTime);
                        
        }
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (!abierta)
            {
                puerta.abierta = true;
            }
        }
    }
}
