using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : InterfaceIA
{
    
    

    private Enemy Yo; 
    
    public InterfaceIA init(Enemy Enemigo)
    {
        Yo = Enemigo;
        
        return this;
    }
    
    public Chase(Enemy Enemigo)
    {
        init(Enemigo);
        Yo = Enemigo;
        Debug.Log("Esto Chase");
        
    }

    public void UpdateState()
    {
        Vector2 newP;
        newP = Vector2.MoveTowards(Yo.transform.position, Yo.Player.transform.position, Yo.speed*Time.deltaTime);
        newP=new Vector2(newP.x,Yo.transform.position.y);
        Yo.transform.position=newP;
        //Debug.Log("A por el Player");
        if (CanAttack())
        {
            Debug.Log("Chase to attac");
            Yo.SetState(new Attack(Yo));;
        }

        if (PlayerScapes())
        {
            Debug.Log("Chase to patrol");
            Yo.SetState(new Patrol(Yo));
        }
        
    }

    bool PlayerScapes()
    {
        bool ret = false;
        
        if (Yo.Player.transform.position.y>Yo.transform.position.y+2|| Vector2.Distance(Yo.Player.transform.position,Yo.transform.position)>Yo.rangeChase+1)
        {
           // Debug.Log("Ha escapado");
            ret = true;
        }
        
        return ret;
    }

    bool CanAttack()
    {
        bool ret = false;
        if (Vector2.Distance(Yo.transform.position, Yo.Player.transform.position) <= Yo.rangetoAttack)
        {
            //Debug.Log("Al attaque");
            ret = true;
        }
        return ret;
    }
}
