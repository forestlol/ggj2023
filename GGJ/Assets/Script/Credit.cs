using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Credit : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI txt_level, txt_Experience;

    private void Start()
    {
        txt_level.text = "Floor Completed - " + GameManager.instance.lastFloor;
        txt_Experience.text = "Total Experience - " + GameManager.instance.lastEXP;
        txt_Experience.text += "\nCharacter Level - " + GameManager.instance.lastLevel;
    }
}
