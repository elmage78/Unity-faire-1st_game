using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.TextCore.Text;

public class Letter_Set  : MonoBehaviour
{
    private string[] Characters = new string[27];
    private TextMeshProUGUI m_TextMeshPro;
    private int j = 0;
    private float Cooldown = 0.2f;
    private float CC = 0f;

    // Start is called before the first frame update
    void Start()
    {
        m_TextMeshPro = GetComponent<TextMeshProUGUI>();
        Characters.SetValue("a", 0);
        Characters.SetValue("b", 1);
        Characters.SetValue("c", 2);
        Characters.SetValue("d", 3);
        Characters.SetValue("e", 4);
        Characters.SetValue("f", 5);
        Characters.SetValue("g", 6);
        Characters.SetValue("h", 7);
        Characters.SetValue("i", 8);
        Characters.SetValue("j", 9);
        Characters.SetValue("k", 10);
        Characters.SetValue("l", 11);
        Characters.SetValue("m", 12);
        Characters.SetValue("n", 13);
        Characters.SetValue("ñ", 14);
        Characters.SetValue("o", 15);
        Characters.SetValue("p", 16);
        Characters.SetValue("q", 17);
        Characters.SetValue("r", 18);
        Characters.SetValue("s", 19);
        Characters.SetValue("t", 20);
        Characters.SetValue("u", 21);
        Characters.SetValue("v", 22);
        Characters.SetValue("w", 23);
        Characters.SetValue("x", 24);
        Characters.SetValue("y", 25);
        Characters.SetValue("z", 26);
    }

    // Update is called once per frame
    void Update()
    {
        if (EventSystem.current.currentSelectedGameObject.GetComponentInChildren<TextMeshProUGUI>() == m_TextMeshPro)  
        {
            if (CC > 0)
            {
                CC -= Time.deltaTime;
            }
            if (Input.GetAxisRaw("Vertical") > 0 && CC <= 0)
            {
                j -= 1;
                CC = Cooldown;
            }
            if (Input.GetAxisRaw("Vertical") < 0 && CC <= 0)
            {
                CC = Cooldown;
                j += 1;
            }
            if (j < 0)
            {
                j = 0;
            }
            if (j > 26)
            {
                j = 0;
            }
        }
        m_TextMeshPro.text = Characters[j];
    }
}
