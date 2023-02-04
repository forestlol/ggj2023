using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_Player : Unit
{

    public override void DoDamage(int damage)
    {
        base.DoDamage(damage);
    }

    protected override void DoDeath()
    {
        base.DoDeath();

        Destroy(gameObject);
    }
}
