using System;
using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class LearnFightSword : Agent
{
    [NonSerialized] public float Xmovement = 0f;

    [SerializeField] private GameObject Enemy;
    public bool HasWon = false;
    Vector2 Direction1 = Vector2.zero;
    Rigidbody2D Rigidbody2D;
    Animator animator;

    [NonSerialized] public bool hasAttacked = false;
    private float timer = 0f;

    private bool jump = false;
    private bool platdown = false;
    public void Start()
    {
        HasWon = false;
        timer = 0f;
        jump = false;
        platdown = false;
        animator = GetComponent<Animator>();
        Rigidbody2D = GetComponent<Rigidbody2D>();
    }
   /* public override void OnEpisodeBegin()
    {
        HasWon = false;
        timer = 0f;
        jump = false;
        platdown = false;
        this.transform.localPosition = new Vector3(6f, 0f, 0f);
    }*/
    public override void CollectObservations(VectorSensor sensor)
    {
        if (this != null)
        {
            sensor.AddObservation(this.transform.localPosition);
        }
        if (Enemy != null)
        {
            sensor.AddObservation(Enemy.transform.localPosition);
        }
        if (Enemy.GetComponent<MasitAI2D>() != null)
        {
            sensor.AddObservation(Enemy.GetComponent<MasitAI2D>().AttackTime);
        }
        else if (Enemy.GetComponent<PlayerAttack2D>() != null)
        {
            sensor.AddObservation(Enemy.GetComponent<PlayerAttack2D>().AttackTimer);
        }
        sensor.AddObservation(timer);
    }
    public override void OnActionReceived(ActionBuffers actions)
    {
        Xmovement = 0f;
        Vector2 Direction = new Vector2(actions.ContinuousActions[0], actions.ContinuousActions[1]);
        Direction1 = Direction;
        MasitAI2D AI2 = this.GetComponent<MasitAI2D>();
        MasitaController Movement = this.GetComponent<MasitaController>();
        if (actions.DiscreteActions[2] == 1)
        {
            AI2.AttackGround(Direction);
        }
        if (actions.DiscreteActions[4] == 1)
        {
            AI2.AttackAir();
        }
        if (actions.DiscreteActions[1] == 1)
        {
            jump = true;
        }
        if (actions.DiscreteActions[3] == 1)
        {
            platdown = true;
        }
        if (actions.DiscreteActions[0] == 0)
        {
            Xmovement = -1f;
        }
        if (actions.DiscreteActions[0] == 1)
        {
            Xmovement = 0f;
        }
        if (actions.DiscreteActions[0] == 2)
        {
            Xmovement = 1f;
        }
        Movement.Move(Xmovement, false, jump, platdown);
        jump = false;
        platdown = false;
    }
    public void Update()
    {
        float DistanceToEnemy = 0f;
        DistanceToEnemy = Mathf.Sqrt(MathF.Pow((Enemy.transform.localPosition.x - this.transform.localPosition.x), 2) + MathF.Pow((Enemy.transform.localPosition.y - this.transform.localPosition.y), 2));
        timer += Time.deltaTime;
      //  AddReward(Time.deltaTime * -0.75f);
        if (HasWon)
        {
            Debug.Log("a");
            HasWon = true;
          //  SetReward(300f);
          //  EndEpisode();
        }
        if (hasAttacked)
        {
          //  AddReward(30f);
            hasAttacked = false;
        }
    }
    private void FixedUpdate()
    {
        if (Rigidbody2D.velocity != Vector2.zero)
        {
            animator.SetFloat("LastXspeed", Rigidbody2D.velocity.x);
            animator.SetFloat("LastYspeed", Rigidbody2D.velocity.y);
            animator.SetBool("HasSpeed", MathF.Abs(Rigidbody2D.velocity.x) + MathF.Abs(Rigidbody2D.velocity.y) != 0);
            animator.SetBool("aereo", !this.GetComponent<MasitaController>().m_Grounded);
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
      /*  if (collision.tag == "DeathZone")
        {
            if (Enemy.GetComponent<LearnFight>() != null)
            {
                Enemy.GetComponent<LearnFight>().HasWon = true;
            }
            if (Enemy.GetComponent<LearnFightSword>() != null)
            {
                Enemy.GetComponent<LearnFightSword>().HasWon = true;
            }
            SetReward(-200f);
            EndEpisode();
        }*/
    }
}
