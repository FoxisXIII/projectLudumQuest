using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  interface InterfaceIA
{
    // Start is called before the first frame update
    void UpdateState();
    InterfaceIA init(Enemy Enemigo);
    
}
