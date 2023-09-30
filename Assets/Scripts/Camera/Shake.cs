using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Shake : MonoBehaviour
{
    float TimeElapsed = 0;
    float InternalDuration = 0;
    CinemachineVirtualCamera Vcam;
    CinemachineBasicMultiChannelPerlin Shaker;

    bool shaking = false;

    private void Start()
    {
        Vcam = gameObject.GetComponent<CinemachineVirtualCamera>();
        Shaker = Vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }
    public void ShakeCamera(float Amplitude, float Frequency, float Duration)
    {
        Shaker.m_AmplitudeGain = Amplitude;
        Shaker.m_FrequencyGain = Frequency;
        shaking = true;
        TimeElapsed = 0f;
        InternalDuration = Duration;
    }
    // Update is called once per frame
    void Update()
    {
        if (shaking)
        {
            TimeElapsed += Time.deltaTime;

            if (TimeElapsed > InternalDuration)
            {
                Shaker.m_AmplitudeGain = 0;
                Shaker.m_FrequencyGain = 0;
            }
        }
    }
}
