using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookingAt : MonoBehaviour
{
    Transform ObjectPos;
    public Transform PlayerPos;
    public float Displacement;

    private void Start()
    {
        ObjectPos = gameObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    private void Update()
    {
        ObjectPos.position = PlayerPos.position + new Vector3(PlayerVariables.LastSpeed.normalized.x,PlayerVariables.LastSpeed.normalized.y, 0f) * Displacement;
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        
    }
}
