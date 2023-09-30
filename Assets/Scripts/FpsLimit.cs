using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class FpsLimit : MonoBehaviour
{
    TextMeshProUGUI TextMeshProUGUI;
    [SerializeField] private Slider slider;
    private void Start()
    {
        TextMeshProUGUI = GetComponent<TextMeshProUGUI>();
    }
    private void Update()
    {
        if (this.GetComponentInParent<Variables>() != null)
        {
            TextMeshProUGUI.text = (slider.value + 80).ToString() + "%";
        }
        else TextMeshProUGUI.text = slider.value.ToString();
    }
}
