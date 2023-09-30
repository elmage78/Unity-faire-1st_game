using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookingAtMasita : MonoBehaviour
{
    Transform PositionOfThis;
    public Transform MasitaPos;
    public Transform PlayerPos;
    [SerializeField] private Rigidbody2D Masita;
    [SerializeField] private float ThisDisplacement;

    // Vars
    private Vector2 LastSpeed;

    private void Start()
    {
        PositionOfThis = gameObject.GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        if (Masita.velocity != Vector2.zero)
        {
            LastSpeed.x = PlayerPos.position.x - MasitaPos.position.x;
            LastSpeed.y = PlayerPos.position.y - MasitaPos.position.y;
        }
        PositionOfThis.position = MasitaPos.position + new Vector3(LastSpeed.x, LastSpeed.y, 0f).normalized * ThisDisplacement;
    }
 }
