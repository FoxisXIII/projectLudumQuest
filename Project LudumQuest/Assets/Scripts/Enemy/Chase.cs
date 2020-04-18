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
       
    }

    public void UpdateState()
    {
        Vector2 newP;
        newP = Vector2.MoveTowards(Yo.transform.position, GameController.getInstance().PlayerController.transform.position, Yo.speed*Time.deltaTime);
        newP=new Vector2(newP.x,Yo.transform.position.y);
        Yo.transform.position=newP;
        //Debug.Log("A por el Player");
        if (CanAttack())
        {
            Yo.SetState(new Attack(Yo));;
        }

        if (PlayerScapes())
        {
            Yo.SetState(new Patrol(Yo));
        }
        
    }

    bool PlayerScapes()
    {
        return GameController.getInstance().PlayerController.transform.position.y>Yo.transform.position.y+2|| Vector2.Distance(GameController.getInstance().PlayerController.transform.position,Yo.transform.position)>Yo.rangeChase+1;
    }

    bool CanAttack()
    {
        return Vector2.Distance(Yo.transform.position, GameController.getInstance().PlayerController.transform.position) <= Yo.rangetoAttack;
    }
}
