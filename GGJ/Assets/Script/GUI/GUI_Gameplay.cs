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

    private void Start()
    {
        GUI_Gameplay.instance.SetLevelTxt(GameManager.instance.current_game_Level);
        GUI_Gameplay.instance.SetExpTxt(GameManager.instance.experience, GameManager.instance.expCap);
    }

    public void SetLevelTxt(int level)
    {
        txt_level.text = "Level - " + level;
    }

    public void SetExpTxt(int exp, int maxexp)
    {
        txt_exp.text = "Exp - " + exp + "/" + maxexp;
    }
}
