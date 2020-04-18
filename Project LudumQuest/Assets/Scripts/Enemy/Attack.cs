using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : InterfaceIA
{
    private Enemy Yo; 
    
    public InterfaceIA init(Enemy Enemigo)
    {
        Yo = Enemigo;
        
        return this;
    }
    
    public Attack(Enemy Enemigo)
    {
        init(Enemigo);
        Yo = Enemigo;
        //Debug.Log("Esto Chase");
        
    }
    public void UpdateState()
    {
        if (Vector2.Distance(Yo.transform.position, Yo.Player.transform.position) >= Yo.rangetoAttack+0.5f)
        {
            Yo.Player.transform.position.Normalize();//DEBERIA ATACAR AQUI
            //Debug.Log("Atacando");
            

        } else{
            Debug.Log("Attac to patrol");
            Yo.SetState(new Patrol(Yo));
        }
        
        
    }
}
