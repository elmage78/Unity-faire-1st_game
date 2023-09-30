using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunInBg : MonoBehaviour
{
    private void Awake()
    {
        Application.runInBackground = true;
    }
}
