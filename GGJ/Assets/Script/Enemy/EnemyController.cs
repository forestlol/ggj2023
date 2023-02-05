using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : Unit
{
    [SerializeField] 
    private Animator animator;

    public Room m_room;

    public override void DoDamage(int damage)
    {
        if (damage > 0)
        {
            GameManager.instance.IncreaseEXP(10);
        }
        base.DoDamage(damage);
    }

    protected override void DoDeath()
    {
        base.DoDeath();
        Debug.Log(gameObject.name + " has Died");
        Destroy(gameObject);

        m_room.Room_EnemyEliminate_Update();
    }

    // VFX / Animation
    void OnHitEffect()
    {
        animator.Play("Fly Take Damage 0");
    }
}
