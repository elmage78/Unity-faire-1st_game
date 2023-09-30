using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerAttack2D : MonoBehaviour
{
    //Unity Objects
    private Animator Animator;
    [SerializeField] private Rigidbody2D MasitaRB;
    private Rigidbody2D rb;
    [SerializeField] private LayerMask EnemyMask;
    //Attacks
    [NonSerialized] public bool AttackGrounded;
    bool AttackAerialLight;
    bool AttackGroundedStrong;

    [Space]

    [SerializeField] private float GroundedRange;
    [SerializeField] private float GroundedAttackSize;
    [SerializeField] private float GroundedKnockBack;

    [Space]

    [SerializeField] private float AttackHitCheckTimer;
    [SerializeField] private float TimeUntilNextAttack;
    //Variables
    Vector2 GizmosVector = Vector2.zero;
    Vector2 LastInput = Vector2.zero;
    Vector2 LastSpeed = Vector2.zero;
    float LastYspeed = 0f;
    float LastXspeed = 0f;
    [NonSerialized] public float AttackTimer;
    private float HitCheckTimer;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
    }
    // Light Attacks
    public void AttackGround() //Light attack also
    {
        AttackGrounded = true;
        HitCheckTimer = AttackHitCheckTimer;
    }
    public void AttackAirLight()
    {
        AttackAerialLight = true;
        HitCheckTimer = AttackHitCheckTimer;
    }
    public void AttackGroundStrong()
    {
        IsHeldDown = false;
        hasAttacked = false;
        AttackCharge = 0f;
        pop = false;
        pop2 = false;
        AttackGroundedStrong = true;
    }
    private void Update()
    {
        if (!AttackGrounded && !AttackAerialLight)
        {
            Animator.SetBool("Attack1", false);
        }
        // Timers
        {
            if (AttackTimer > 0)
            {
                AttackTimer -= Time.deltaTime;
            }
            if (HitCheckTimer > 0)
            {
                HitCheckTimer -= Time.deltaTime;
            }
        }

        //Getting inputs
        if (Input.GetMouseButtonDown(0))
        {
            Animator.SetBool("Attack1", true);
        }
      /*  if (Input.GetMouseButtonDown(1))
        {
            Animator.SetBool("Attack2", true);
        }*/
    }

    //Extra variables for strong attack
    bool IsHeldDown = false;
    bool hasAttacked = false;
    float AttackCharge = 0f;
    bool pop = false;
    bool pop2 = false;
    float Multiplier = 0f;
    private void FixedUpdate()
    {
        // STRONG ATTACK GROUND
        if (AttackGroundedStrong && AttackTimer <= 0)
        {
            rb.velocity = Vector2.zero;
          //  IsHeldDown = Input.GetMouseButton(1);

            if (IsHeldDown && !pop2)
            {
                rb.velocity = Vector2.zero;
                pop = true;
                AttackCharge += Time.deltaTime;
                if (AttackCharge > 3f)
                {
                    HitCheckTimer = AttackHitCheckTimer;
                    Animator.SetBool("ChangeToStrong", true);
                    IsHeldDown = false;
                }
            }
            if (pop && !IsHeldDown && !pop2)
            {
                Multiplier = AttackCharge;
                Debug.Log(Multiplier);
                pop2 = true;
                HitCheckTimer = AttackHitCheckTimer;
                rb.velocity = Vector2.zero;
                Animator.SetBool("ChangeToStrong", true);
            }

            if (HitCheckTimer > 0)
            {
                rb.velocity = Vector2.zero;
                hasAttacked = true;
                GameObject HittedEntity = null;
                bool hashit = false;
                if (LastInput.y < 0f)
                {
                    LastInput.y = 0f;
                    LastInput.x = LastXspeed;
                }
                if (LastInput.x == 0f)
                {
                    LastInput.x = LastXspeed;
                }
                if (LastInput.y == 0f)
                {
                    LastInput.y = LastXspeed;
                }
                Vector2 Position = new Vector2(this.transform.position.x, this.transform.position.y) + LastInput * GroundedRange;
                Collider2D[] HitChecks = Physics2D.OverlapCircleAll(Position, GroundedAttackSize, EnemyMask);
                foreach (Collider2D hitCheck in HitChecks)
                {
                    if (hitCheck.enabled) hashit = true;
                    HittedEntity = hitCheck.gameObject;
                }
                if (HittedEntity != null && hashit == true)
                {
                    Vector2 LaunchDir = Vector2.zero;
                    LaunchDir.x = HittedEntity.transform.position.x - this.transform.position.x;
                    LaunchDir.y = HittedEntity.transform.position.y - this.transform.position.y;
                    LaunchDir = LaunchDir.normalized * GroundedKnockBack;
                    LaunchDir.y += LaunchDir.x * 0.1f;
                    LaunchDir *= 0.8f;
                    Debug.Log("LaunchDir");
                    HittedEntity.GetComponent<MasitAI2D>().TakeKnockBack(LaunchDir*(Multiplier+1.0f));
                    if (HittedEntity.GetComponent<MasitaController>() != null)
                    {
                        HittedEntity.GetComponent<MasitaController>().TimePushed = 0.5f;
                    }
                    if (HittedEntity.GetComponent<CharacterController2D>() != null)
                    {
                        HittedEntity.GetComponent<CharacterController2D>().TimePushed = 0.5f;
                    }
                }
            }

            if (HitCheckTimer <= 0 && hasAttacked)
            {
                Debug.Log("Attack end");
                AttackGroundedStrong = false;
                AttackTimer = TimeUntilNextAttack;
                Animator.SetBool("Attack2", false);
                Animator.SetBool("ChangeToStrong", false);
                Multiplier = 0f;
            }
        }
        // Variables of Animator
        if (rb.velocity != Vector2.zero)
        {
            LastInput.x = Input.GetAxisRaw("Horizontal");
            LastInput.y = Input.GetAxisRaw("Vertical");
            Animator.SetFloat("LastXInput", LastInput.x);
            Animator.SetFloat("LastYInput", LastInput.y);
            LastSpeed.x = rb.velocity.x;
            LastSpeed.y = rb.velocity.y;
            Animator.SetFloat("LastXSpeed", LastSpeed.x);
            Animator.SetFloat("LastYSpeed", LastSpeed.y);
        }
        Animator.SetBool("HasSpeed", Mathf.Abs(LastInput.x) + Mathf.Abs(LastInput.y) != 0);
        if (rb.velocity.y != 0f)
        {
            LastYspeed = rb.velocity.y;
        }
        if (rb.velocity.x != 0f)
        {
            LastXspeed = rb.velocity.x;
        }
        LastInput = LastInput.normalized;
        //Attack when on ground
        if (AttackGrounded && AttackTimer <= 0)
        {
            if ( HitCheckTimer > 0)
            {
                rb.velocity = Vector2.Lerp(rb.velocity, Vector2.zero, 0.2f);
                Vector2 Inp = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
                GameObject HittedEntity = null;
                bool hashit = false;
                if (LastInput.y < 0f)
                {
                    LastInput.y = 0f;
                    LastInput.x = LastXspeed;
                    Inp = LastInput;
                }
                Inp = Inp.normalized;
                if (Inp == Vector2.zero)
                {
                    Inp.x = LastXspeed;
                    Inp = Inp.normalized; 
                }
                Vector2 Position = new Vector2(this.transform.position.x, this.transform.position.y) + Inp * GroundedRange;
                Collider2D[] HitChecks = Physics2D.OverlapCircleAll(Position, GroundedAttackSize, EnemyMask);
                GizmosVector = Position;
                foreach (Collider2D hitCheck in HitChecks)
                {
                    if (hitCheck.enabled) hashit = true;
                    HittedEntity = hitCheck.gameObject;
                }
                if (HittedEntity != null && hashit == true)
                {
                    Vector2 LaunchDir = Vector2.zero;
                    LaunchDir.x = HittedEntity.transform.position.x - this.transform.position.x;
                    LaunchDir.y = HittedEntity.transform.position.y - this.transform.position.y;
                    LaunchDir = LaunchDir.normalized * GroundedKnockBack;
                    LaunchDir.y += LaunchDir.x * 0.1f;
                    HittedEntity.GetComponent<MasitAI2D>().TakeKnockBack(LaunchDir);
                    if (HittedEntity.GetComponent<MasitaController>() != null)
                    {
                        HittedEntity.GetComponent<MasitaController>().TimePushed = 0.5f;
                    }
                    if (HittedEntity.GetComponent<CharacterController2D>() != null)
                    {
                        HittedEntity.GetComponent<CharacterController2D>().TimePushed = 0.5f;
                    }
                }
            }
            if (HitCheckTimer <= 0)
            {
                AttackGrounded = false;
                AttackTimer = TimeUntilNextAttack;
                Animator.SetBool("Attack1", false);
            }
        }

         // AERIAL ATTACK
        if (AttackAerialLight && AttackTimer <= 0)
        {
            if (HitCheckTimer > 0)
            {
                GameObject HittedEntity = null;
                bool hashit = false;
                Vector2 Position = new Vector2(this.transform.position.x, this.transform.position.y);
                Collider2D[] HitChecks = Physics2D.OverlapCircleAll(Position, 2f, EnemyMask);
                foreach (Collider2D hitCheck in HitChecks)
                {
                    if (hitCheck.enabled) hashit = true;
                    HittedEntity = hitCheck.gameObject;
                }
                if (HittedEntity != null && hashit == true)
                {
                    Vector2 LaunchDir = Vector2.zero;
                    LaunchDir.x = HittedEntity.transform.position.x - this.transform.position.x;
                    LaunchDir.y = HittedEntity.transform.position.y - this.transform.position.y;
                    LaunchDir = LaunchDir.normalized * GroundedKnockBack;
                    LaunchDir.y += LaunchDir.x * 0.1f;
                    HittedEntity.GetComponent<MasitAI2D>().TakeKnockBack(LaunchDir);
                    if (HittedEntity.GetComponent<MasitaController>() != null)
                    {
                        HittedEntity.GetComponent<MasitaController>().TimePushed = 0.5f;
                    }
                    if (HittedEntity.GetComponent<CharacterController2D>() != null)
                    {
                        HittedEntity.GetComponent<CharacterController2D>().TimePushed = 0.5f;
                    }
                }
            }
            if (HitCheckTimer <= 0)
            {
                AttackAerialLight = false;
                Animator.SetBool("Attack1", false);
                Animator.SetBool("Attack2", false);
                AttackTimer = TimeUntilNextAttack;
            }
        }
    }

    // Debug things
    private void OnDrawGizmos()
    {
    }
}