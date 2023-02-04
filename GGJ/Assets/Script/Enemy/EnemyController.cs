using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : Unit
{
    [SerializeField] private Animator animator;

    public override void DoDamage(int damage)
    {
        GameManager.instance.IncreaseEXP(10);
        base.DoDamage(damage);
    }

    protected override void DoDeath()
    {
        base.DoDeath();
        Debug.Log(gameObject.name + " has Died");
        Destroy(gameObject);
    }

    // VFX / Animation
    void OnHitEffect()
    {
        animator.Play("Fly Take Damage 0");
    }
}
