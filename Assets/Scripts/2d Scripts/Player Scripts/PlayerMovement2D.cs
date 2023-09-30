using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement2D : MonoBehaviour
{
    public CharacterController2D Controller;
    float HorizontalMove = 0f;
    bool jump = false;
    bool crouch = false;
    [Space]
    public float runSpeed;

    private Rigidbody2D rb;
    private Animator animator;

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        HorizontalMove = Input.GetAxisRaw("Horizontal");
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            crouch = true;
        }
    }
    private void FixedUpdate()
    {
        if (!this.GetComponent<PlayerAttack2D>().AttackGrounded)
        Controller.Move(HorizontalMove * Time.fixedDeltaTime * runSpeed,false,jump,crouch);
        jump = false;
        crouch = false;
        ChangeAnimations();
    }
    private void ChangeAnimations()
    {
    }
}
