using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FpsLimiter : MonoBehaviour
{
    private Slider Slider;
    // Start is called before the first frame update
    void Start()
    {
        Slider = this.GetComponent<Slider>();   
    }

    // Update is called once per frame
    void Update()
    {
        Application.targetFrameRate = (int)Slider.value;
    }
}
