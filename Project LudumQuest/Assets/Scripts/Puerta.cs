using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puerta : MonoBehaviour
{

    public bool abierta = false;
    public float speed = 1f;
    public Vector2 posPressed;
    
    // Start is called before the first frame update
    void Start()
    {
        posPressed = new Vector2(transform.position.x, transform.position.y + 4);
    }

    // Update is called once per frame
    void Update()
    {

        if (abierta)
        {
            transform.position=Vector2.MoveTowards(transform.position, posPressed, speed*Time.deltaTime);
            
        }
    }
    
    
    
}
