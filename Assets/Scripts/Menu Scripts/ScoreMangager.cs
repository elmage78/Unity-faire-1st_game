using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreMangager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI ScoreText;

    // Start is called before the first frame update
    void Start()
    {
        ScoreText.text = PlayerVariables.Score.ToString() + " Puntos";
    }
    private void Update()
    {
        ScoreText.text = PlayerVariables.Score.ToString() + " Puntos";
    }
}
