using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour
{
    [Header("Fire point")]
    [SerializeField]
    Transform spawnPoint;

    [Header("Bullet Prefab")]
    [SerializeField]
    Rigidbody m_bullet;

    [Space]

    [Header("Bullet Statistics")]
    [SerializeField, Range(0.05f, 1f)]
    float fireRate;
    [SerializeField, Range(1,10)]
    int bulletDamage;
    [SerializeField, Range(1,10)]
    int bulletSpeed;

    Rigidbody bullet;
    bool canFire = true;

    // Update is called once per frame
    void Update()
    {
        if (!Input.GetMouseButton(0))
            return;

        if (!canFire)
            return;

        StartCoroutine(Fire());
    }

    IEnumerator Fire()
    {
        canFire = false;
        bullet = Instantiate(m_bullet, spawnPoint.position, spawnPoint.rotation);
        bullet.AddForce(spawnPoint.forward * bulletSpeed, ForceMode.Impulse);
        bullet.GetComponent<Bullet>().SpawnBullet(bulletDamage);

        yield return new WaitForSeconds(fireRate);
        canFire = true;
    }
}
