using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool IsPaused = false;

    public GameObject PauseMenuUI;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsPaused)
            {
                Resume();
            }
            if (!IsPaused)
            {
                Pause();
            }
        }
    }
    public void Resume()
    {
        Time.timeScale = 1;
        IsPaused = false;
        PauseMenuUI.SetActive(false);
    }
    void Pause()
    {
        Time.timeScale = 0f;
        IsPaused = true;
        PauseMenuUI.SetActive(true);
    }
}
