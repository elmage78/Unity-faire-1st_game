using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Second_Portal_Int : MonoBehaviour
{
    [SerializeField] Continuara Continue;
    bool HasRun;

    [System.Obsolete]
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Interact" && PlayerVariables.Interact == true)
        {
            if (HasRun)
            {
            } else
            {
                Continue.Continuara1();
                HasRun = true;
            }
        }
    }
}
