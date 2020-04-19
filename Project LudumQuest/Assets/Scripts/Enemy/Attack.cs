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
        Yo._animator.SetBool("WALK", false);
        Yo._animator.SetBool("ATTACK", true);
    }

    public void UpdateState()
    {
        time += Time.deltaTime;
        if (time >= Yo.attackRate)
        {
            Yo._animator.SetBool("ATTACK", true);
            time = 0;
        }
        Yo.transform.rotation = Quaternion.Lerp(Yo.transform.rotation, Quaternion.Euler(0, Yo.rotation, 0),
            2 * Time.deltaTime);
        if (Vector2.Distance(Yo.transform.position, GameController.getInstance().PlayerController.transform.position) >= Yo.rangetoAttack)
            Yo.SetState(new Chase(Yo));
    }
}