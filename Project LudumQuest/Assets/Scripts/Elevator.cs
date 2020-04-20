using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public float speed = 1f;
    public bool needsActivation;
    public bool needsPlayer;
    public Interactuable[] interactuable;
    private Vector2 initialPosition;
    public Vector2 finalPosition;
    private bool move;
    private bool goToFinal = true;
    private bool goToInitial;

    private void Start()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
        if (needsActivation && On() || needsPlayer && move || !needsActivation && !needsPlayer)
        {
            if(goToFinal)
            {
                transform.position = Vector2.MoveTowards(transform.position, finalPosition, speed * Time.deltaTime);
                if (Math.Abs(transform.position.x - finalPosition.x) < .1f &&
                    Math.Abs(transform.position.y - finalPosition.y) < .1f)
                {
                    goToInitial = true;
                    goToFinal = false;
                }
            }
            if(goToInitial)
            {
                transform.position = Vector2.MoveTowards(transform.position, initialPosition, speed * Time.deltaTime);
                if (Math.Abs(transform.position.x - initialPosition.x) < .1f &&
                    Math.Abs(transform.position.y - initialPosition.y) < .1f)
                {
                    goToFinal = true;
                    goToInitial = false;
                }
            }
        }

        if (!move && needsPlayer)
        {
            transform.position = Vector2.MoveTowards(transform.position, initialPosition, speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.isTrigger && other.gameObject.CompareTag("Player"))
        {
            other.transform.parent = transform;
            move = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.isTrigger && other.gameObject.CompareTag("Player"))
        {
            other.transform.parent = null;
            move = false;
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