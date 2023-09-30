using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BootPickup : MonoBehaviour
{
    public GameObject Items;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Interact" && PlayerVariables.Interact == true)
        {
            Items.SetActive(false);
            PlayerVariables.SpeedChange = 1.6f;
        }
    }
}
