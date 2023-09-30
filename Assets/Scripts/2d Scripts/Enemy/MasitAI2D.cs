using System;
using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents.Integrations.Match3;
using Unity.VisualScripting;
using UnityEngine;

public class MasitAI2D : MonoBehaviour
{
    private bool m_FacingRight = true;
    //Unity Stuff
    Rigidbody2D MasitaRB;
    LearnFight LearnFight;
    LearnFightSword LearnFightSword;
    Animator animator;

    //Variables
    float ITime;
    float HitCheckTimer = 0f;
    float AttackCC = 0.6f;
    float TimeAttack = 0f;
    [NonSerialized] public float AttackTime = 2.75f;
    Vector2 Direction1 = Vector2.zero;

    Vector2 DebugGizmo = Vector2.zero;

    // Attacking Stuff
    [SerializeField] private float InmortaliltyTime;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        MasitaRB = GetComponent<Rigidbody2D>();
        if (LearnFight != null)
        {
            LearnFight = GetComponent<LearnFight>();
        }
        if (LearnFightSword != null)
        {
            LearnFightSword = GetComponent<LearnFightSword>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (ITime > 0)
        {
            ITime -= Time.deltaTime;
        }
        if (HitCheckTimer > 0f)
        {
            HitCheckTimer -= Time.deltaTime;
            animator.SetBool("Attacking", true);
        }
        else animator.SetBool("Attacking", false);

        if (TimeAttack > 0f)
        {
            TimeAttack -= Time.deltaTime;
        }
    }
    public void TakeKnockBack(Vector2 KnockBack)
    {
        if (ITime <= 0)
        {
            ITime = InmortaliltyTime;
            MasitaRB.velocity = Vector2.zero;
            MasitaRB.AddForce(KnockBack);
        }
    }
    //Variables
    private bool Started = false;
    private bool Started1 = false;
    private bool AttackGrounded = false;
    private bool AttackAerial = false;
    //AttackGround
    public void AttackGround(Vector2 Direction)
    {
        Direction1 = Direction;
        AttackGrounded = true;
        Started = false;
    }
    public void AttackAir()
    {
        AttackAerial = true;
        Started1 = false;
    }
    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void FixedUpdate()
    {
        float move = 0f;
        if (LearnFightSword != null)
        {
            move = LearnFightSword.Xmovement;
        }
        if (LearnFight != null)
        {
            move = LearnFight.Xmovement;
        }
        if (move > 0 && !m_FacingRight)
        {
            // ... flip the enemy.
            Flip();
        }
        // Otherwise if the input is moving the player left and the player is facing right...
        else if (move < 0 && m_FacingRight)
        {
            // ... flip the enemy.
            Flip();
        }

        if (AttackGrounded)
        {
            if (this.GetComponent<MasitaController>().m_Grounded)
            {
                if (!Started && TimeAttack <= 0 && HitCheckTimer <= 0f)
                {
                    HitCheckTimer = AttackCC;
                }
                if (HitCheckTimer > 0 && TimeAttack <= 0)
                {
                    MasitaRB.velocity = Vector2.Lerp(MasitaRB.velocity, Vector2.zero, 0.2f);
                    GameObject HittedEntity = null;
                    bool hashit = false;
                    animator.SetFloat("AttackX", Direction1.x);
                    animator.SetFloat("AttackY", Direction1.y);
                    Vector2 Position = new Vector2(this.transform.position.x, this.transform.position.y) + Direction1.normalized * 1.3f;
                    DebugGizmo = Position;
                    Collider2D[] HitChecks = Physics2D.OverlapCircleAll(Position, 1f);
                    foreach (Collider2D hitCheck in HitChecks)
                    {
                        if (hitCheck.enabled) hashit = true;
                        HittedEntity = hitCheck.gameObject;
                    }
                    if (HittedEntity != null && hashit == true && HittedEntity != this.gameObject)
                    {
                        Vector2 LaunchDir = Vector2.zero;
                        LaunchDir.x = HittedEntity.transform.position.x - this.transform.position.x;
                        LaunchDir.y = HittedEntity.transform.position.y - this.transform.position.y;
                        LaunchDir = LaunchDir.normalized * 250;
                        LaunchDir.y += LaunchDir.x * 0.1f;
                        LaunchDir *= 3f;
                        if (HittedEntity.GetComponent<MasitaController>() != null)
                        {
                            HittedEntity.GetComponent<MasitaController>().TimePushed = 1f;
                        }
                        if (HittedEntity.GetComponent<CharacterController2D>() != null)
                        {
                            HittedEntity.GetComponent<CharacterController2D>().TimePushed = 0.5f;
                        }
                        if (HittedEntity.GetComponent<MasitAI2D>() != null)
                        {
                            HittedEntity.GetComponent<MasitAI2D>().TakeKnockBack(LaunchDir);
                        }
                        if (HittedEntity.GetComponent<CharacterController2D>() != null)
                        {
                            HittedEntity.GetComponent<CharacterController2D>().TakeKnockBack(LaunchDir);
                        }
                        if (this.GetComponent<LearnFight>() != null)
                        {
                            this.GetComponent<LearnFight>().hasAttacked = true;
                        }
                        if (this.GetComponent<LearnFightSword>() != null)
                        {
                            this.GetComponent<LearnFightSword>().hasAttacked = true;
                        }
                    }
                }
                if (HitCheckTimer <= 0 && TimeAttack >= 0)
                {
                    AttackGrounded = false;
                    TimeAttack = AttackTime;
                }
                Started = true;
            }
        }
        if (AttackAerial)
        {
            if (!this.GetComponent<MasitaController>().m_Grounded)
            {
                if (!Started1 && TimeAttack <= 0f && HitCheckTimer <= 0f)
                {
                    HitCheckTimer = AttackCC;
                }
                if (HitCheckTimer > 0 && TimeAttack <= 0)
                {
                    GameObject HittedEntity = null;
                    bool hashit = false;

                    Vector2 Position = new Vector2(this.transform.position.x, this.transform.position.y);
                    Collider2D[] HitChecks = Physics2D.OverlapCircleAll(Position, 2f);
                    foreach (Collider2D hitCheck in HitChecks)
                    {
                        if (hitCheck.enabled) hashit = true;
                        HittedEntity = hitCheck.gameObject;
                    }
                    if (HittedEntity != null && hashit == true && HittedEntity != this.gameObject)
                    {
                        Vector2 LaunchDir = Vector2.zero;
                        LaunchDir.x = HittedEntity.transform.position.x - this.transform.position.x;
                        LaunchDir.y = HittedEntity.transform.position.y - this.transform.position.y;
                        LaunchDir = LaunchDir.normalized * 250;
                        LaunchDir.y += LaunchDir.x * 0.1f;
                        LaunchDir *= 3f;
                        if (HittedEntity.GetComponent<MasitaController>() != null)
                        {
                            HittedEntity.GetComponent<MasitaController>().TimePushed = 1f;
                        }
                        if (HittedEntity.GetComponent<CharacterController2D>() != null)
                        {
                            HittedEntity.GetComponent<CharacterController2D>().TimePushed = 0.5f;
                        }
                        if (HittedEntity.GetComponent<MasitAI2D>() != null)
                        {
                            HittedEntity.GetComponent<MasitAI2D>().TakeKnockBack(LaunchDir);
                        }
                        if (HittedEntity.GetComponent<CharacterController2D>() != null)
                        {
                            HittedEntity.GetComponent<CharacterController2D>().TakeKnockBack(LaunchDir);
                        }
                        if (this.GetComponent<LearnFight>() != null)
                        {
                            this.GetComponent<LearnFight>().hasAttacked = true;
                        }
                        if (this.GetComponent<LearnFightSword>() != null)
                        {
                            this.GetComponent<LearnFightSword>().hasAttacked = true;
                        }
                    }
                }
                if (HitCheckTimer <= 0 && TimeAttack >= 0)
                {
                    AttackAerial = false;
                    TimeAttack = AttackTime;
                }
                Started1 = true;
            }
        }
    }
    public void OnDrawGizmos()
    {
    }
}
