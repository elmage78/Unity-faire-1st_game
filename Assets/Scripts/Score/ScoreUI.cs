using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Scripting;

public class ScorUi : MonoBehaviour
{
    public RowUI rowUI;
    public ScoreManag scoreMangag;
    private void Start()
    {
        var scores = scoreMangag.GetHighScores().ToArray();

        for (int i = 0; i < scores.Length; i++)
        {
            var row = Instantiate(rowUI,transform).GetComponent<RowUI>();
            row.Name.text = scores[i].Name;
            row.Score.text = scores[i].score.ToString();
            if (i > 10)
            {
                break;
            }
        }
    }
}
