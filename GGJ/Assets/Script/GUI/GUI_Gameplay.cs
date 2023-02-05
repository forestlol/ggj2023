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
        GUI_Gameplay.instance.SetExpTxt(GameManager.instance.experience, GameManager.instance.expCap, GameManager.instance.GetCurrentLevel());
    }

    public void SetLevelTxt(int floor)
    {
        txt_level.text = "Floor - " + floor;
    }

    public void SetExpTxt(int exp, int maxexp, int level)
    {
        txt_exp.text = "Level - " + level + "\nExp - " + exp + "/" + maxexp;
    }
}
