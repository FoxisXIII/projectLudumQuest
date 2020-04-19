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
    public GameObject leftPoint, rightPoint;
    public float attackRate;
    public Image inkLevel;

    private Material inkMaterial;
    public float attack;
    public GameObject prefabink;

    [HideInInspector] public Animator _animator;


    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        Status = new Patrol(this);
        inkMaterial = Instantiate<Material>(GameController.getInstance().InkMaterial);
        inkMaterial.SetFloat("_Fade", .5f);
        foreach (var child in transform.GetComponentsInChildren<SpriteRenderer>())
        {
            if (child.material.name.Equals(inkMaterial.name))
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

    public void TakeDamage(float damage)
    {
        _animator.SetTrigger("TAKE_DAMAGE");
        inkLevel.fillAmount = Mathf.Max(inkLevel.fillAmount - damage, 0);

        inkMaterial.SetFloat("_Fade", inkLevel.fillAmount / 2);

        if (inkLevel.fillAmount <= 0)
        {
            Instantiate(prefabink, transform).transform.parent = null;
            Destroy(gameObject);
        }
    }

    public void Attack()
    {
        GameController.getInstance().PlayerController.TakeDamage(attack);
    }

    public void RandomAttack()
    {
        _animator.SetBool("ATTACK", false);
        _animator.SetFloat("ATTACK_COMBOS", Random.Range(1, 4));
    }
}