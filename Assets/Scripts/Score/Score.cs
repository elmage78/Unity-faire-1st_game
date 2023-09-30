using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Score
{
    public string Name;
    public float score;

    public Score(string name, float score)
    {
        this.Name = name;
        this.score = score;
    }
}
