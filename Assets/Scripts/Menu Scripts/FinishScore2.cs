using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FiinishScore1 : MonoBehaviour
{
    private TextMeshProUGUI TextScore;
    // Start is called before the first frame update
    void Start()
    {
        TextScore = GetComponent<TextMeshProUGUI>();
        TextScore.text = PlayerVariables.Score.ToString() + " Puntos" + "\n";
    }

    // Update is called once per frame
    void Update()
    {

    }
}
