using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Patrol : InterfaceIA
{
    private float rotation;

    private Enemy Yo;

    public InterfaceIA init(Enemy Enemigo)
    {
        Yo = Enemigo;

        return this;
    }

    public Patrol(Enemy Enemigo)
    {
        init(Enemigo);
        Yo = Enemigo;

        Yo._animator.SetBool("WALK", true);
    }

    public void UpdateState()
    {
        if (Yo.leftPoint.transform.position.x <= Yo.transform.position.x)
        {
            rotation = 180;
            Yo.ActualP = 1;
        }

        if (Yo.rightPoint.transform.position.x >= Yo.transform.position.x)
        {
            rotation = 0;
            Yo.ActualP = 0;
        }

        Yo.transform.rotation = Quaternion.Lerp(Yo.transform.rotation, Quaternion.Euler(0, rotation, 0),
            2 * Time.deltaTime);
        
        if (Yo.ActualP == 0)
        {
            //Debug.Log("Move to P1");
            Vector2 newP;
            newP = Vector2.MoveTowards(Yo.transform.position, Yo.leftPoint.transform.position, Yo.speed * Time.deltaTime);
            newP = new Vector2(newP.x, Yo.transform.position.y);
            Yo.transform.position = newP;
        }
        else if (Yo.ActualP == 1)
        {
            // Debug.Log("Move to P2");
            Vector2 newP;
            newP = Vector2.MoveTowards(Yo.transform.position, Yo.rightPoint.transform.position, Yo.speed * Time.deltaTime);
            newP = new Vector2(newP.x, Yo.transform.position.y);
            Yo.transform.position = newP;
        }

        if (SeePlayer())
        {
            Yo.SetState(new Chase(Yo));
        }
    }

    bool SeePlayer()
    {
        bool ret = false;

        if (Vector2.Distance(Yo.transform.position, GameController.getInstance().PlayerController.transform.position) <=
            Yo.rangeChase)
        {
            ret = true;
        }

        return ret;
    }
}