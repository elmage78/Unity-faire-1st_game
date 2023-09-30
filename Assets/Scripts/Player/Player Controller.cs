using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] DeathMenu DeathMenu;
    [SerializeField] GameObject Bg;
    // Components
    Rigidbody2D rb;
    SpriteRenderer rb2D;
    Animator animator;

    //variables
    Vector2 LastSpeed;
    float lastXSpeed;
    float lastYSpeed;
    public float Wspeed;
    float inputHorizontal;
    float inputVertical;
    bool interact;
    bool attack1;
    bool attack2;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();  
        animator = gameObject.GetComponent<Animator>();
        rb2D = gameObject.GetComponent<SpriteRenderer>();
    }
    private void Awake()
    {
        if (PlayerVariables.LastPosition != null && PlayerVariables.LastPosition != Vector2.zero)
        {
            this.transform.position = PlayerVariables.LastPosition;
        }
    }
    // Update is called once per frame
    void Update()
    {
        //setting inputs
        interact = Input.GetKeyDown(KeyCode.E);
        attack1 = Input.GetMouseButtonDown(0);
        attack2 = Input.GetMouseButtonDown(1);
        if (interact)
        {
            for (int i = 0; i < 10; i++)
            {
                PlayerVariables.Interact = interact;
            }
        }

        inputHorizontal = Input.GetAxisRaw("Horizontal");
        inputVertical = Input.GetAxisRaw("Vertical");
        //update animations
        ChangeAnimations();
        //functions
        if (PauseMenu.IsPaused)
        {
            AudioListener Listener = this.GetComponent<AudioListener>();
            Listener.enabled = false;
        }
        else
        {
            AudioListener Listener = this.GetComponent<AudioListener>();
            Listener.enabled = true;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        rb.velocity = Vector2.zero;
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(inputHorizontal, inputVertical).normalized * Wspeed * Time.fixedDeltaTime * PlayerVariables.SpeedChange;
        if (AttackAnimation.AttackAnimationActive == true )
        {
            rb.velocity = Vector2.zero;
        }
        ChangeAnimations();
    }

    // Anims

    void ChangeAnimations()
    {
        // setting variables for animations
        if (rb.velocity != Vector2.zero)
        {
            lastXSpeed = rb.velocity.x;
            lastYSpeed = rb.velocity.y;
            LastSpeed = new Vector2 (lastXSpeed, lastYSpeed);
            animator.SetFloat("LastXSpeed", lastXSpeed);
            animator.SetFloat("LastYSpeed", lastYSpeed);
            PlayerVariables.LastSpeed = LastSpeed;
            PlayerVariables.LastXspeed = lastXSpeed;
            PlayerVariables.LastYspeed = lastYSpeed;
        }
        animator.SetBool("HasSpeed", Mathf.Abs(inputHorizontal) + Mathf.Abs(inputVertical) != 0);
        animator.SetFloat("Xspeed", rb.velocity.x);
        animator.SetFloat("Yspeed", rb.velocity.y);
        animator.SetBool("attack1", attack1);
        animator.SetBool("Interact", interact);
        if (lastXSpeed > 0)
        {
            rb2D.flipX = false;
        }
        else if (lastXSpeed < 0)
        {
            rb2D.flipX = true;
        }
        // Global variables getting "published"
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        bool IsSword = false;
        if (collision.GetComponentInParent<ScoreXtra2>() != null)
        {
            IsSword = true;
        }
        if (collision.CompareTag("Enemy") && collision.GetComponentInParent<MasitAI>().attacking)
        {

            Bg.SetActive(true);
            PlayerVariables.LastPosition = this.transform.position;
            if (IsSword)
            {
                DeathMenu.NextStage(SceneManager.GetActiveScene().buildIndex + 2);
            } else DeathMenu.NextStage(SceneManager.GetActiveScene().buildIndex + 1);
            collision.GetComponentInParent<MasitAI>().attacking = false;
        }
    }
}
