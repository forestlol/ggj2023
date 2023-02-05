using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SceneChanger : MonoBehaviour
{
    public static SceneChanger instance;

    [Space]
    [SerializeField]
    GameObject loadingScreen;

    [SerializeField]
    Image _progressBar;

    AsyncOperation operation;

    public void Awake()
    {
        instance = this;
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadLevel(sceneName));
    }

    IEnumerator LoadLevel(string sceneName){
        loadingScreen.SetActive(true);
        operation = SceneManager.LoadSceneAsync(sceneName);

        while(!operation.isDone){
            _progressBar.fillAmount = Mathf.Clamp01(operation.progress / 0.9f);
            yield return null;
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
