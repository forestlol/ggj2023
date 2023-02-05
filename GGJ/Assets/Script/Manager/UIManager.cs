using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public GameObject UIGroup;

    public TMP_Text currentLevelText;
    public TMP_Text experienceText;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    public void ShowGameOverPanel()
    {
        UIGroup.SetActive(true);

        currentLevelText.text = "Floor: " + GameManager.instance.GetFloor().ToString();
        experienceText.text = "Total Experience: " + GameManager.instance.GetExperience().ToString() + "\n" +
                              "Character Level: " + GameManager.instance.GetCurrentLevel().ToString();
    }

    public void HideGameOverPanel()
    {
        UIGroup.SetActive(false);
    }

    public void OnMenuClicked()
    {
        AudioManager.instance.PlaySound("click", transform);
        SceneChanger.instance.LoadScene("Main Menu");
    }
}
