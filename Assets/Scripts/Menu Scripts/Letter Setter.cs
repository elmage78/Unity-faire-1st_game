using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class LetterSetter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Letter1;
    [SerializeField] private TextMeshProUGUI Letter2;
    [SerializeField] private TextMeshProUGUI Letter3;
    [SerializeField] private TextMeshProUGUI Letter4;
    // Start is called before the first frame update
    [SerializeField] ScoreManag ScoreManag;
    [SerializeField] GameObject Arrows;
    [SerializeField] EventSystem EventSystem;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Arrows.transform.position = EventSystem.currentSelectedGameObject.transform.position;
    }
    public void LetterFinish()
    {
        ScoreManag.AddScore(new Score(Letter1.text + Letter2.text + Letter3.text + Letter4.text, PlayerVariables.Score)); 
    }
}
