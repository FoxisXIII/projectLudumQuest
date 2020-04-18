using System.Collections;
using System.Collections.Generic;
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
             Debug.Log("Esto Patrulla");
             
         }

    public void UpdateState()
    {
        

        
        if (Yo.pointP1.transform.position.x <= Yo.transform.position.x)
        {
            //Debug.Log("Change to P2");
            Yo.ActualP = 1;
        }
        if (Yo.pointP2.transform.position.x >= Yo.transform.position.x)
        {
            //Debug.Log("Change to P1");
            Yo.ActualP = 0;
        }
        
        if (Yo.ActualP==0)
        {
            //Debug.Log("Move to P1");
            Vector2 newP;
            newP = Vector2.MoveTowards(Yo.transform.position, Yo.pointP1.transform.position, Yo.speed*Time.deltaTime);
            newP=new Vector2(newP.x,Yo.transform.position.y);
            Yo.transform.position=newP;
        }else if (Yo.ActualP==1)
        {
           // Debug.Log("Move to P2");
            Vector2 newP;
            newP = Vector2.MoveTowards(Yo.transform.position, Yo.pointP2.transform.position, Yo.speed*Time.deltaTime);
            newP=new Vector2(newP.x,Yo.transform.position.y);
            Yo.transform.position=newP;
        }
        
        if (SeePlayer())
        {
            Debug.Log("Cambio a CHase");
            Yo.SetState(new Chase(Yo));
        }

    }

    bool SeePlayer()
    {
        bool ret=false;

        if (Vector2.Distance(Yo.transform.position, Yo.Player.transform.position) <= Yo.rangeChase)
        {
            ret = true;
        }
        
        return ret;
    }

   
}
