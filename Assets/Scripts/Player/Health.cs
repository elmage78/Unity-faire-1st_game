using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [NonSerialized] public Slider HealthDisplay;
    // Start is called before the first frame update
    void Start()
    {
        HealthDisplay = gameObject.GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        HealthDisplay.value = PlayerVariables.health;
        if (PlayerVariables.health <= 0)
        {
            SceneManager.LoadScene(1);
        }
    }
}
