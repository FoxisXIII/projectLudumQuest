using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puerta : MonoBehaviour
{
    public float speed = 1f;
    public Interactuable[] interactuable;
    private Vector2 posPressed;
    public bool byEnemy;
    public GameObject enemy;

    // Start is called before the first frame update
    void Start()
    {
        posPressed = new Vector2(transform.position.x, transform.position.y + 7);
    }

    // Update is called once per frame
    void Update()
    {
        if (byEnemy && enemy == null || !byEnemy && On())
        {
            transform.position = Vector2.MoveTowards(transform.position, posPressed, speed * Time.deltaTime);
        }
    }

    private bool On()
    {
        foreach (var VARIABLE in interactuable)
        {
            if (!VARIABLE.On())
                return false;
        }

        return true;
    }
}