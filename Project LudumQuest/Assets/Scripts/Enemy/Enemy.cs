using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public InterfaceIA Status;
    public int ActualP = 0;
    public float rangeChase = 5f;
    public float rangetoAttack = 1f;
    public float speed = 0.2f;
    public GameObject pointP1, pointP2;
    public float attackRate;
    public Image inkLevel;

    private Material inkMaterial;
    public float attack;


    // Start is called before the first frame update
    void Start()
    {
        Status = new Patrol(this);
        inkMaterial = Instantiate<Material>(GameController.getInstance().InkMaterial);
        inkMaterial.SetFloat("_Fade", .5f);
        foreach (var child in transform.GetComponentsInChildren<SpriteRenderer>())
        {
            child.material = inkMaterial;
        }
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

    public bool TakeDamage(float damage)
    {
        inkLevel.fillAmount = Mathf.Max(inkLevel.fillAmount - damage, 0);

        inkMaterial.SetFloat("_Fade", inkLevel.fillAmount / 2);

        return inkLevel.fillAmount <= 0;
    }
}