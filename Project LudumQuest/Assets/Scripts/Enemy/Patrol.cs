using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEditor;
using UnityEngine;

public class Patrol : InterfaceIA
{
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
        Yo.rotation = 180;
        Yo._animator.SetBool("WALK", true);
    }

    public void UpdateState()
    {
        Yo.transform.rotation = Quaternion.Lerp(Yo.transform.rotation, Quaternion.Euler(0, Yo.rotation, 0),
            2 * Time.deltaTime);

        if (Yo.ActualP == 0)
        {
            //Debug.Log("Move to P1");
            Vector2 newP;
            newP = Vector2.MoveTowards(Yo.transform.position, Yo.leftPoint,
                Yo.speed * Time.deltaTime);
            newP = new Vector2(newP.x, Yo.transform.position.y);
            Yo.transform.position = newP;
            
            if(Yo.leftPoint.x==Yo.transform.position.x)
            {
                Yo.rotation = 0;
                Yo.ActualP = 1;
            }
        }
        else if (Yo.ActualP == 1)
        {
            // Debug.Log("Move to P2");
            Vector2 newP;
            newP = Vector2.MoveTowards(Yo.transform.position, Yo.rightPoint,
                Yo.speed * Time.deltaTime);
            newP = new Vector2(newP.x, Yo.transform.position.y);
            Yo.transform.position = newP;
            if(Yo.rightPoint.x==Yo.transform.position.x)
            {
                Yo.rotation = 180;
                Yo.ActualP = 0;
            }
        }

        if (SeePlayer())
        {
            Yo.SetState(new Chase(Yo));
        }
    }

    bool SeePlayer()
    {
        return Mathf.Abs(Yo.transform.position.y - GameController.getInstance().PlayerController.transform.position.y) <
            .1f && Vector2.Distance(Yo.transform.position,
                GameController.getInstance().PlayerController.transform.position) <=
            Yo.rangeChase;
    }
}