using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeathMenu : MonoBehaviour
{
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private GameObject mainMenu;

    [SerializeField] private Slider LoadingSlider;

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    [System.Obsolete]
    public void retry(string LevlToLoad)
    {
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            Scene[] Scenes = SceneManager.GetAllScenes();
            foreach (Scene Ascene in Scenes)
            {
                if (Ascene.isLoaded)
                {
                    if (Ascene.buildIndex != SceneManager.GetActiveScene().buildIndex)
                    {
                        SceneManager.UnloadSceneAsync(Ascene);
                    }
                }
            }
        }
        mainMenu.SetActive(false);
        loadingScreen.SetActive(true);

        //Run the Async
        StartCoroutine(LoadLevelAsync(LevlToLoad));
    }
    public void NextStage(int LevlToLoad)
    {
        mainMenu.SetActive(false);
        loadingScreen.SetActive(true);

        //Run the Async
        StartCoroutine(LoadLevelAsync1(LevlToLoad));
    }
    IEnumerator LoadLevelAsync(string LevelToLoad)
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(LevelToLoad);

        while (!loadOperation.isDone)
        {
            float progressValue = Mathf.Clamp01(loadOperation.progress / 0.9f);
            LoadingSlider.value = progressValue;
            yield return null;
        }
    }
    IEnumerator LoadLevelAsync1(int LevelToLoad)
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(LevelToLoad);

        while (!loadOperation.isDone)
        {
            float progressValue = Mathf.Clamp01(loadOperation.progress / 0.9f);
            LoadingSlider.value = progressValue;
            yield return null;
        }
    }
}
