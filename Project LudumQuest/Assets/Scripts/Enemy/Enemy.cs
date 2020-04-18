using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public InterfaceIA Status;
    public int ActualP = 0;
    public float rangeChase = 5f;
    public float rangetoAttack = 1f;
    public float speed = 0.2f;
    public GameObject pointP1, pointP2, Player;
    public float attackRate;
    
    
    // Start is called before the first frame update
    void Start()
    {
        Status= new Patrol(this);
    }

    // Update is called once per frame
    void Update()
    {
        Status.UpdateState();
    }

    public void SetState(InterfaceIA statusnew)
    {
        Status = statusnew;
    }

}
