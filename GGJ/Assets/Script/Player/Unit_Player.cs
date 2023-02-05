using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Unit_Player : Unit
{
    public override void DoDamage(int damage)
    {
        base.DoDamage(damage);
    }

    protected override void DoDeath()
    {
        base.DoDeath();


        UIManager.instance.ShowGameOverPanel();
        Destroy(gameObject);
    }

    public void IncreaseHealth(int value)
    {
        maxHealth += value;
        health += value;
    }
}
