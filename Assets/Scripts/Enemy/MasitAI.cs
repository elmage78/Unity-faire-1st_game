using Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents.Integrations.Match3;
using Unity.VisualScripting;
using UnityEngine;

public class MasitAI : MonoBehaviour
{
    [NonSerialized] public bool attacking;
    AIPath AIPath;


    bool m_FacingRight = true;
    float a = 0;
    public Transform Player;
    Transform Self;
    Rigidbody2D body;
    List<string> AIs = new List<string>();
    string ActualAI;
    int RandomNum;
    float e622 = 0;
    float MoveX = 0;
    float MoveY = 0;
    float IFrames = 0;
    Animator animator;
    [NonSerialized] public bool BeingPushed;
    // Stats
    public int Health = 5;
    public float RandomSpeed;
    [Range(0f,2f)] public float InmunityTime;
    //Attack
    public bool attack = false;
    AIPath Path;

    void Start()    
    {
        AIs.Add("Idle");
        AIs.Add("Advance");
        AIs.Add("Advance Smart");

        RandomNum = UnityEngine.Random.Range(0, AIs.Count);
        RandomSpeed = UnityEngine.Random.Range(55f, 150f);

        Self = gameObject.GetComponent<Transform>();
        body = gameObject.GetComponent<Rigidbody2D>();
        Path = this.GetComponent<AIPath>();
        animator = this.GetComponent<Animator>();
        AIPath = this.GetComponent<AIPath>();
    }
    void Kill(GameObject Self)
    {
        Destroy(Self);
    }
    private void Update()
    {
        if (attacking)
        {
            a += Time.deltaTime;
            if (a >= 0.1f)
            {
                attack = false;
                attacking = false;
                a = 0f;
            }
        }
        if (IFrames > 0)
        {
            IFrames -= Time.deltaTime;
        }
        // Check distance to se attack
        float x2 = Player.position.x - Self.position.x;
        float y2 = Player.position.y - Self.position.y;
        float distance = (float)System.Math.Sqrt((double)(x2 * x2 + y2 * y2));
        //Change inmunity frames

        if (IFrames <= 0)
        {
            BeingPushed = false;
        }
        //Check death
        if (Health <= 0)
        {
            PlayerVariables.Score += 1;
            if (this.gameObject.GetComponent<ScoreXtra>() != null)
            {
                PlayerVariables.Score += 1;
            }
            if (this.gameObject.GetComponent<ScoreXtra2>() != null)
            {
                PlayerVariables.Score += 3;
            }

            Kill(this.gameObject);
        }

        //Count and change ais randomly
        e622++;
        if (e622 >= 10000)
        {
            RandomNum = UnityEngine.Random.Range(0, AIs.Count);
        }
        //attack
        if (!attack && distance < 1.2f)
        {
            attack = true;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Changeanimations();
        if (this.GetComponent<SpriteRenderer>().color == Color.red)
        {
            this.GetComponent<SpriteRenderer>().color = Color.white;
        }
        // Check distance to se attack
        float x2 = Player.position.x - Self.position.x;
        float y2 = Player.position.y - Self.position.y;
        float distance = (float)System.Math.Sqrt((double)(x2 * x2 + y2 * y2));
        if (BeingPushed)
        {
            this.GetComponent<SpriteRenderer>().color = Color.red;
            body.velocity = Vector2.Lerp(body.velocity, Vector2.zero, 0.1f);
            Path.enabled = false;
        }
        if (!BeingPushed) 
        {
            Path.enabled = true;
            ActualAI = AIs[RandomNum];
            if (ActualAI == "Advance")
            {
                body.velocity = new Vector2(x2, y2).normalized * RandomSpeed * Time.fixedDeltaTime;
            }
            if (ActualAI == "Advance Smart")
            {
                distance = (float)System.Math.Sqrt((double)(x2 * x2 + y2 * y2));
                if (distance < 3.4f)
                {
                    Vector2 Speed = new Vector2(x2, y2).normalized * RandomSpeed * Time.fixedDeltaTime;
                    body.velocity = Speed;
                }
                else
                {
                    if (e622 % 344 == 344)
                    {
                        MoveX = UnityEngine.Random.Range(-2f, 2f);
                        MoveY = UnityEngine.Random.Range(-2f, 2f);
                    }
                    body.velocity = new Vector2(MoveX, MoveY).normalized * RandomSpeed * Time.fixedDeltaTime;
                }
            }
        }
        //attack stop
        if (attack)
        {
            if (BeingPushed)
            {
                attack = false;
            }
            if (!BeingPushed)
            {
                body.velocity = Vector2.zero;
                AIPath.maxSpeed = 0f;
            }
        } else AIPath.maxSpeed = 2.02f;
    }
     // Attacked
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Interact") && AttackAnimation.AttackAnimationActive == true && IFrames <= 0f)
        {
            Health -= PlayerVariables.damage;
            // X and Y from PLAYER to ENEMY
            float x2 = - Player.position.x + Self.position.x;
            float y2 = - Player.position.y + Self.position.y;
            body.AddForce(new Vector2 (x2, y2).normalized * PlayerVariables.knockback);
            BeingPushed = true;
            IFrames = InmunityTime;
        }
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
    private void Changeanimations()
    {
        //Flip Masita
        if (body.velocity.x > 0 && !m_FacingRight)
        {
            // ... flip the enemy.
            Flip();
        }
        // Otherwise if the input is moving the player left and the player is facing right...
        else if (body.velocity.x < 0 && m_FacingRight)
        {
            // ... flip the enemy.
            Flip();
        }

        //Animator variables
        if (attack && !attacking)
        {
            animator.SetBool("IsAttacking", true);
        } else animator.SetBool("IsAttacking", false);
        animator.SetBool("HasSpeed", MathF.Abs(body.velocity.x) + MathF.Abs(body.velocity.y) != 0);
    }
    public void Attack1()
    {
        attacking = true;
    }
}
