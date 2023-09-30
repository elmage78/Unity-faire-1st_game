using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordPickup : MonoBehaviour
{
    public GameObject Items;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Interact" && PlayerVariables.Interact == true)
        {
            Items.SetActive(false);
            PlayerVariables.damage *= 2;
            PlayerVariables.knockback *= 2;
        }
    }
}
