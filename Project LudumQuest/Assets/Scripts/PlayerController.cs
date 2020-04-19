using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.IMGUI.Controls;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpSpeed;

    [SerializeField] private float rotationSpeed;

    private bool inGround;
    private bool climbLadder;

    private float rotation;
    private Rigidbody2D _rigidbody;

    public float ink;
    public GameObject prefabink;
    [SerializeField] private float maxInk;

    [SerializeField] private Material inkMaterial;

    public float inkSpeed;


    public TextMeshProUGUI inkLevelTextUI;
    public Image inkLevelImageUI;
    public TextMeshProUGUI inkSpeedUI;

    public float damage;
    private Enemy enemy;

    private Animator _animator;
    private bool jump;
    private bool push;
    private int pushPos;

    public AudioClip[] sounds;

    private AudioSource _audioSource;

    // Start is called before the first frame update
    void Awake()
    {
        GameController.getInstance().PlayerController = this;
        ink = maxInk;
        _rigidbody = GetComponent<Rigidbody2D>();
        GameController.getInstance().InkMaterial = inkMaterial;
        inkMaterial = Instantiate<Material>(GameController.getInstance().InkMaterial);

        foreach (var child in transform.GetComponentsInChildren<SpriteRenderer>())
        {
            if (!child.gameObject.name.Equals("Eyes"))
                child.material = inkMaterial;
        }

        _animator = GetComponent<Animator>();

        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ink > 0)
        {
            Movement();

            if (Input.GetKeyDown(KeyCode.Mouse0))
                AttackEnemies();
        }

        Ink();
    }

    private void AttackEnemies()
    {
        if (!_animator.GetBool("JUMP"))
            _animator.SetBool("ATTACK", true);
    }

    private void Ink()
    {
        ink -= Mathf.Max(inkSpeed * Time.deltaTime, 0);
        if (ink <= 0)
        {
            GameController.getInstance().LevelManager.Defeat();
        }

        inkLevelImageUI.fillAmount = ink;
        inkLevelTextUI.text = (int) (ink * 100) + "%";
        inkSpeedUI.text = "x" + (inkSpeed * 100).ToString("F2");
        if (inkSpeed * 100 > 3) inkSpeedUI.color = Color.red;
        else inkSpeedUI.color = Color.black;


        inkMaterial.SetFloat("_Fade", ink / 2);
    }

    public void TakeDamage(float damage)
    {
        _animator.SetTrigger("TAKE_DAMAGE");
        inkSpeed = Mathf.Min(inkSpeed + damage, 0.05f);
    }

    private void Movement()
    {
        Vector2 movement = new Vector2();
        if (!_animator.GetBool("ATTACK"))
        {
            if (Input.GetKey(KeyCode.D))
            {
                movement.x = speed * Time.deltaTime;
                rotation = 0;
                if (push && pushPos > 0)
                    _animator.SetBool("PUSH", true);
                else
                    _animator.SetBool("WALK", true);
            }

            if (Input.GetKey(KeyCode.A))
            {
                movement.x = -speed * Time.deltaTime;
                rotation = 180;
                if (push && pushPos < 0)
                    _animator.SetBool("PUSH", true);
                else
                    _animator.SetBool("WALK", true);
            }

            if (movement.x == 0)
            {
                _animator.SetBool("PUSH", false);
                _animator.SetBool("WALK", false);
                _animator.SetBool("IDLE", true);
            }

            movement.y = _rigidbody.velocity.y;
            if (inGround && !climbLadder && Input.GetKey(KeyCode.Space))
            {
                _animator.SetBool("WALK", false);
                _animator.SetBool("JUMP", true);
            }
            else if (!inGround)
            {
                _animator.SetBool("FALL", true);
            }
            else
            {
                _animator.SetBool("FALL", false);
                _animator.SetBool("IDLE", true);
            }

            if (jump)
            {
                movement.y = jumpSpeed;
                jump = false;
            }

            if (climbLadder)
            {
                _animator.SetBool("CLIMB_LADDER", true);
                movement.y = 0;
                if (Input.GetKey(KeyCode.W))
                {
                    movement.y = speed * Time.deltaTime;
                }

                if (Input.GetKey(KeyCode.S))
                {
                    movement.y = -speed * Time.deltaTime;
                }
            }
            else
            {
                _animator.SetBool("CLIMB_LADDER", false);
            }
        }

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, rotation, 0),
            rotationSpeed * Time.deltaTime);
        _rigidbody.velocity = movement;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Floor":
                inGround = true;
                break;
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Pushable":
                push = true;
                if (transform.position.y <= other.transform.position.y)
                {
                    if ((Mathf.Abs(other.transform.position.x) > Mathf.Abs(transform.position.x) && rotation == 0) ||
                        (Mathf.Abs(other.transform.position.x) < Mathf.Abs(transform.position.x) && rotation == 180))
                    {
                        other.rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0);
                        pushPos = Mathf.Abs(other.transform.position.x) > Mathf.Abs(transform.position.x) ? 1 : -1;
                    }
                }
                else
                {
                    inGround = true;
                }

                break;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Floor":
                inGround = false;
                break;
            case "Pushable":
                inGround = false;
                push = false;
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Enemy":
                enemy = other.GetComponent<Enemy>();
                break;
            case "Victory":
                GameController.getInstance().LevelManager.Victory();
                break;
            case "Ladder":
                _rigidbody.gravityScale = 0;
                climbLadder = true;
                inGround = true;
                break;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Enemy":
                enemy = null;
                break;
            case "Ladder":
                _rigidbody.gravityScale = 5;
                climbLadder = false;
                inGround = false;
                break;
        }
    }

    public void Jump()
    {
        if (!jump)
        {
            jump = true;
            _audioSource.clip = sounds[2];
            _audioSource.Play();
        }
    }

    public void StopJump()
    {
        _animator.SetBool("JUMP", false);
        _audioSource.clip = sounds[3];
        _audioSource.Play();
    }

    public void Attack()
    {
        if (enemy != null &&
            ((Mathf.Abs(enemy.transform.position.x) > Mathf.Abs(transform.position.x) && rotation == 0) ||
             (Mathf.Abs(enemy.transform.position.x) < Mathf.Abs(transform.position.x) && rotation == 180)))
        {
            _audioSource.clip = sounds[5];
            _audioSource.Play();
            enemy.TakeDamage(damage);
        }
    }

    public void RandomAttack()
    {
        _animator.SetBool("ATTACK", false);
        _animator.SetBool("IDLE", true);
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