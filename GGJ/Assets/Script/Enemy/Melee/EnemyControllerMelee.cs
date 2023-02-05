using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CartoonFX;

public enum MeleeClass { Melee, Suicidal }

public class EnemyControllerMelee : EnemyController
{
    [Space]
    [SerializeField]
    private int damage = 1;

    [Space]
    [SerializeField]
    private float speed = 1;

    [Space]
    [SerializeField]
    private float radius = 10;

    [Space]
    public MeleeClass meleeClass;

    [Header("Damage Effect")]
    public CFXR_ParticleText effect;

    public GameObject hitEffect;

    private void Update()
    {
        // Would add a check if game is over here
        //if (GameManager.instance.isGameOver) {
        //    return;
        //}

        LayerMask worldLayer = LayerMask.NameToLayer("Player");
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius, 1 << worldLayer);

        if (hitColliders.Length == 0) {
            return;
        }


        Vector3 direction;
        direction = hitColliders[0].transform.position - transform.position;
        direction.y = 0;
        transform.forward = direction;

        transform.position += speed * Time.deltaTime * direction.normalized;
    }

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

        if (hitEffect != null) {
            Vector3 spawnPosition = transform.position + new Vector3(0, 0.25f, 0);
            Vector3 damageSpawnPosition = transform.position + new Vector3(0, 1f, 0);

            Instantiate(hitEffect, spawnPosition, Quaternion.identity);

            CFXR_ParticleText temp = Instantiate(effect, damageSpawnPosition, Quaternion.identity);
            temp.UpdateText(damage.ToString());

            Destroy(temp.gameObject, .5f);
        }

        Debug.Log(gameObject.name + " has Died");
        Destroy(gameObject);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) {
            Debug.Log("Dealt " + damage + " damage to player");

            switch (meleeClass) {
                case MeleeClass.Melee:
                    other.GetComponent<Unit>().DoDamage(damage);
                    break;
                case MeleeClass.Suicidal:
                    other.GetComponent<Unit>().DoDamage(damage * 2);
                    DoDeath();
                    break;
            }
        }
    }

    //void OnDrawGizmos()
    //{
    //    // Draw a yellow sphere at the transform's position
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawSphere(transform.position, radius);
    //}
}
