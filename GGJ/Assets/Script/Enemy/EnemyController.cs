using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private int enemyHealth = 100;

    [SerializeField] private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int _damage)
    {
        OnHitEffect();

        enemyHealth -= _damage;
        GameManager.instance.IncreaseEXP(10);

        if (enemyHealth <= 0) {
            Debug.Log(gameObject.name + " has Died");
            Destroy(gameObject);
        }
    }

    // VFX / Animation
    void OnHitEffect()
    {
        animator.Play("Fly Take Damage 0");
    }
}
