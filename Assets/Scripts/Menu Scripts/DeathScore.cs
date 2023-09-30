using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeathScore : MonoBehaviour
{
    private TextMeshProUGUI TextScore;
    // Start is called before the first frame update
    void Start()
    {
        TextScore = GetComponent<TextMeshProUGUI>();
        TextScore.text = PlayerVariables.Score.ToString() +" Puntos" +"\n" +" Has muerto no se guardaran";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
