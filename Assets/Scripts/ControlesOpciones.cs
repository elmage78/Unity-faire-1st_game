using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;

public class ControlesOpciones : MonoBehaviour
{
    public AudioMixer AudioMixer;
    public void SetVolume(float volume)
    {
        AudioMixer.SetFloat("Volume", volume);
    }
    public void SetFullScreen(bool IsFullScreen)
    {
        Screen.fullScreen = IsFullScreen;
    }
}
