using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private int bulletDamage;

    [SerializeField] private GameObject bulletHitEffect;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 1);
    }

    public void SpawnBullet(int _damage)
    {
        bulletDamage = _damage;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy")) {



            if (bulletHitEffect != null) {
                Vector3 spawnPosition = transform.position + new Vector3(0, 0.25f, -0.25f);
                GameObject bulletObject = Instantiate(bulletHitEffect, spawnPosition, Quaternion.identity);
                Destroy(bulletObject, 0.5f);
            }

            Destroy(gameObject);
        }
    }
}
