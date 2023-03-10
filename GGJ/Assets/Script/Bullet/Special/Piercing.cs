using CartoonFX;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piercing : Bullet
{
    public override void Start()
    {
        Destroy(this.gameObject, 3);
    }
    public override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyController>().DoDamage(bulletDamage);

            if (bulletHitEffect != null)
            {
                Vector3 spawnPosition = transform.position + new Vector3(0, 0.25f, 0);
                Vector3 damageSpawnPosition = transform.position + new Vector3(0, 1f, 0);
                Instantiate(bulletHitEffect, spawnPosition, Quaternion.identity);
                CFXR_ParticleText temp = Instantiate(damageText, damageSpawnPosition, Quaternion.identity);
                temp.UpdateText(bulletDamage.ToString());
                Destroy(temp.gameObject, .5f);
            }
        }
    }

    public new void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
    }
}
