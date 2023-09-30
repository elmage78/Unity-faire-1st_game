using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalInteract : MonoBehaviour
{
    [SerializeField] DeathMenu Death;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Interact" && PlayerVariables.Interact == true)
        {
            PlayerVariables.LastPosition = Vector2.zero;
            Death.NextStage(SceneManager.GetActiveScene().buildIndex + 2);
        }
    }
}
