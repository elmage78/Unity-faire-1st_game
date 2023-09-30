using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCollisions : MonoBehaviour
{
    private Rigidbody2D rb2D;
    private CompositeCollider2D Collider;
    private float timer = 0;
    Collider2D[] CollisionsUser = new Collider2D[2];
    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        Collider = GetComponent<CompositeCollider2D>();
    }
    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            for (int i = 0; i < CollisionsUser.Length; i++)
            {
                if (CollisionsUser[i] != null)
                {
                    Physics2D.IgnoreCollision(Collider, CollisionsUser[i], false);
                }
            }
        }

    }

    public void FallPlatform(GameObject User, float time)
    {
        timer = time;
        Rigidbody2D Userrb = User.GetComponent<Rigidbody2D>();
        CollisionsUser = new Collider2D[Userrb.attachedColliderCount];
        Userrb.GetAttachedColliders(CollisionsUser);

        if ( timer > 0 )
        {
            for (int i = 0; i < CollisionsUser.Length; i++)
            {
                Physics2D.IgnoreCollision(Collider, CollisionsUser[i], true);
            }
        }
    }

}
