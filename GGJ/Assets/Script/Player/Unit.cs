using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Unit : MonoBehaviour
{
    public float maxHealth;
    public float health = 10;

    public TMP_Text health_txt;
    public Image hpSlider;

    private void Start()
    {
        maxHealth = health;
        hpSlider.fillAmount = health;
    }

    public virtual void DoDamage(int damage)
    {
        health -= damage;
        health_txt.text = health.ToString();
        hpSlider.fillAmount -= damage;

        if (health <= 0)
        {
            DoDeath();
        }
    }

    protected virtual void DoDeath()
    {

    }
}
