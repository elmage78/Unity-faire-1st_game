using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Continuara : MonoBehaviour
{
    //Unity Objects
    [SerializeField] private GameObject Points;
    [SerializeField] private GameObject Enviroment;
    [SerializeField] private Slider SliderText;
    [SerializeField] private GameObject ContinuaraSpace;
    [SerializeField] ParticleSystem Particles;
    [SerializeField] private Shake Shake;
    [SerializeField] private Image BGimage;


    [System.Obsolete]
    public void Continuara1()
    {
        Enviroment.SetActive(false);    
        this.gameObject.SetActive(true);
        Particles.enableEmission = false;
        ContinuaraSpace.SetActive(false);
        Shake.ShakeCamera(1, 3, 0.2f);
        Points.SetActive(false);
        BGimage.color = Color.white;
        StartCoroutine(ContinuaraCode("Win Menu"));
    }

    [System.Obsolete]
    IEnumerator ContinuaraCode(string LevelToLoad)
    {
        while (BGimage.color != Color.black)
        {
            BGimage.color = Color.Lerp(BGimage.color, Color.black, 5f * Time.deltaTime);
            Debug.Log("lol");
            yield return null;
        }
        if (BGimage.color == Color.black)
        {
            ContinuaraSpace.SetActive(true);
            Particles.enableEmission = true;

            while (SliderText.value > 0)
            {
                SliderText.value -= Time.deltaTime * 0.5f;
                if (SliderText.value < 0.05f)
                {
                    SliderText.value = 0;
                    Particles.enableEmission = false;
                    SceneManager.LoadScene("Win Menu");
                }
                yield return null;
            }
        }
    }
}
