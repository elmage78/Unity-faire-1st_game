using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SonidosNivel1 : MonoBehaviour
{
    private AudioSource[] Audio = new AudioSource[3];
    private float time = 0f;
    private float time2 = 0f;

    private float timeplaying2 = 0f;
    private float timeplaying1 = 0f;

    // Start is called before the first frame update
    void Start()
    {
       Audio = this.GetComponents<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Audio[0].isPlaying)
        {
            timeplaying1 += Time.deltaTime;
        }
        if (Audio[1].isPlaying)
        {
            timeplaying2 += Time.deltaTime;
        }
        if (timeplaying1 > 4.5f)
        {
            Audio[0].volume *= 0.95f;
        }
        if (timeplaying2 > 9.5f)
        {
            Audio[1].volume *= 0.97f;
        }
        time += Time.deltaTime;
        time2 += Time.deltaTime;
        if (time >= 1f)
        {
            time = 0f;
            if (Random.Range(0,8) == 0)
            {
                Audio[0].volume = Random.Range(0.3f, 0.9f);
                if (!Audio[0].isPlaying )
                {
                    Audio[0].Play();
                }
            }
        }
        if (time2 >= 21.1f)
        {
            time2 = 0f;
            Audio[1].volume = Random.Range(0.8f,1f);
            Audio[1].pitch = Random.Range(0.8f, 1f);
            Audio[1].Play();
        }
    }
}
