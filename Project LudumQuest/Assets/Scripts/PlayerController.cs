using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    [SerializeField] private float inkSpeed;


    public TextMeshProUGUI inkLevelTextUI;
    public Image inkLevelImageUI;
    public TextMeshProUGUI inkSpeedUI;

    public float damage;
    private Enemy enemy;

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
            child.material = inkMaterial;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Ink();
        if (Input.GetKeyDown(KeyCode.Mouse0))
            AttackEnemies();
    }

    private void AttackEnemies()
    {
        print("Attack");
        if ((Mathf.Abs(enemy.transform.position.x) > Mathf.Abs(transform.position.x)) && rotation == 0 ||
            (Mathf.Abs(enemy.transform.position.x) < Mathf.Abs(transform.position.x)) && rotation == 180)
            if (enemy.TakeDamage(damage))
            {
                
                Instantiate(prefabink,enemy.transform.position+new Vector3(2,0,0),Quaternion.identity);
                Destroy(enemy.gameObject);
            }
    }

    private void Ink()
    {
        ink -= Mathf.Max(inkSpeed * Time.deltaTime, 0);
        if (ink <= 0)
        {
            Destroy(gameObject);
        }

        inkLevelImageUI.fillAmount = ink;
        inkLevelTextUI.text = (int) (ink * 100) + "%";
        inkSpeedUI.text = "x" + inkSpeed * 100;


        inkMaterial.SetFloat("_Fade", ink / 2);
    }

    public void TakeDamage(float damage)
    {
        inkSpeed *= damage;
    }

    private void Movement()
    {
        Vector2 movement = new Vector2();
        if (Input.GetKey(KeyCode.D))
        {
            movement.x = speed * Time.deltaTime;
            rotation = 0;
        }

        if (Input.GetKey(KeyCode.A))
        {
            movement.x = -speed * Time.deltaTime;
            rotation = 180;
        }


        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, rotation, 0),
            rotationSpeed * Time.deltaTime);

        movement.y = _rigidbody.velocity.y;
        if (inGround && !climbLadder && Input.GetKey(KeyCode.Space))
        {
            movement.y = jumpSpeed;
        }

        if (climbLadder)
        {
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

        _rigidbody.velocity = movement;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Floor":
                inGround = true;
                break;
            case "Ladder":
                _rigidbody.gravityScale = 0;
                climbLadder = true;
                inGround = false;
                break;
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Pushable":
                if (transform.position.y <= other.transform.position.y)
                    other.rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0);
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
            case "Ladder":
                _rigidbody.gravityScale = 5;
                climbLadder = false;
                break;
            case "Pushable":
                inGround = false;
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
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Enemy":
                enemy = null;
                break;
        }
    }
}