using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ink : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerStay2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                other.GetComponent<PlayerController>().ink += 0.1f;
                if (other.GetComponent<PlayerController>().ink>1)
                {
                    other.GetComponent<PlayerController>().ink = 1;
                }
                Destroy(this);
            }
        }
    
}
