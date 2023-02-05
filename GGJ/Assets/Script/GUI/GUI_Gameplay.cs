using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GUI_Gameplay : MonoBehaviour
{
    public static GUI_Gameplay instance;

    [SerializeField]
    TextMeshProUGUI txt_level, txt_exp;

    private void Awake()
    {
        instance = this;
    }

    public void SetLevelTxt(int level)
    {
        txt_level.text = "Level - " + level;
    }

    public void SetExpTxt(int exp)
    {
        txt_exp.text = "Exp - " + exp;
    }
}
