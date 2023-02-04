using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public float health = 10;

    public virtual void DoDamage(int damage)
    {
        health -= damage;

        if(health <= 0)
        {
            DoDeath();
        }
    }

    protected virtual void DoDeath()
    {

    }
}
