using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private int bulletDamage;

    private GameObject bulletHitEffect;

    public void SpawnBullet(int _damage)
    {
        bulletDamage = _damage;
    }
    public void SetHitEffect(GameObject effect)
    {
        bulletHitEffect = effect;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy")) {
            other.GetComponent<EnemyController>().TakeDamage(bulletDamage);

            if (bulletHitEffect != null) {
                Vector3 spawnPosition = transform.position + new Vector3(0, 0.25f, 0);
                GameObject bulletObject = Instantiate(bulletHitEffect, spawnPosition, Quaternion.identity);
                //Destroy(bulletObject, 0.5f);
            }

            Destroy(gameObject);
        }
    }
}
