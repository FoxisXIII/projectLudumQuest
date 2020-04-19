using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    public InterfaceIA Status;
    public int ActualP = 0;
    public float rangeChase = 5f;
    public float rangetoAttack = 1f;
    public float speed = 0.2f;
    [HideInInspector] public Vector2 leftPoint, rightPoint;
    public float attackRate;
    private float inkLevel = 1;

    private Material inkMaterial;
    public float attack;
    public GameObject prefabink;

    [HideInInspector] public Animator _animator;
    public float rotation;
    private AudioSource _audioSource;
    public AudioClip[] sounds;


    // Start is called before the first frame update
    void Start()
    {
        leftPoint = new Vector2(transform.position.x - 5, transform.position.y);
        rightPoint = new Vector2(transform.position.x + 5, transform.position.y);
        _animator = GetComponent<Animator>();
        Status = new Patrol(this);
        inkMaterial = Instantiate<Material>(GameController.getInstance().InkMaterial);
        inkMaterial.SetFloat("_Fade", .5f);
        foreach (var child in transform.GetComponentsInChildren<SpriteRenderer>())
        {
            child.material = inkMaterial;
        }

        _audioSource = GetComponent<AudioSource>();
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
        inkLevel = Mathf.Max(inkLevel - damage, 0);

        inkMaterial.SetFloat("_Fade", inkLevel / 2);

        if (inkLevel <= 0.15f)
        {
            Instantiate(prefabink, transform.position + Vector3.up / 2, quaternion.identity).transform.parent = null;
            Destroy(gameObject);
        }
    }

    public void Attack()
    {
        _audioSource.clip = sounds[5];
        _audioSource.Play();
        GameController.getInstance().PlayerController.TakeDamage(attack);
    }

    public void RandomAttack()
    {
        _animator.SetBool("ATTACK", false);
        _animator.SetFloat("ATTACK_COMBOS", Random.Range(1, 4));
    }

    public void PlayFootstep1()
    {
        _audioSource.clip = sounds[0];
        _audioSource.Play();
    }

    public void PlayFootstep2()
    {
        _audioSource.clip = sounds[1];
        _audioSource.Play();
    }

    public void PlayLadderstep()
    {
        _audioSource.clip = sounds[4];
        _audioSource.Play();
    }

    public void PlayPush()
    {
        _audioSource.clip = sounds[6];
        _audioSource.Play();
    }
}