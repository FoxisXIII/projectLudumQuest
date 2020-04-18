using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : InterfaceIA
{
    private Enemy Yo;
    private float time;

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
        time += Time.deltaTime;
        if (time >= Yo.attackRate)
        {
            Yo.Player.transform.position.Normalize(); //DEBERIA ATACAR AQUI
            Debug.Log("Atacando");
            time = 0;
        }

        if (Vector2.Distance(Yo.transform.position, Yo.Player.transform.position) >= Yo.rangetoAttack)
            Yo.SetState(new Chase(Yo));
    }
}