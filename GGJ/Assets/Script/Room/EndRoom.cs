using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndRoom : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            GameManager.instance.Game_CompletRoom();
            SceneChanger.instance.LoadScene("Scene_Prototype");
        }
    }
}
