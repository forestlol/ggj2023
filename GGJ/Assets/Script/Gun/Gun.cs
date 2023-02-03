using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    int bulletDamage;
    int numberOfBullet;
    int bulletSpeed;

    float fireRate;
    float coolDown;

    Transform spawnPoint;

    Rigidbody m_bullet;

    GameObject hitEffect;
    bool canFire = true;

    public void AddStats (Weapon weapon, Transform spawnPoint)
    {
        this.spawnPoint = spawnPoint;
        this.bulletDamage = weapon.bulletDamage;
        this.numberOfBullet = weapon.numberOfBullet;
        this.bulletSpeed = weapon.bulletSpeed;
        this.fireRate = weapon.fireRate;
        this.coolDown = weapon.coolDown;
        this.m_bullet = weapon.bullet;
        this.hitEffect = weapon.hitEffect;
    }

    // Update is called once per frame
    void Update()
    {
        if (!canFire)
            return;

        StartCoroutine(Fire());
    }

    IEnumerator Fire()
    {
        canFire = false;
        for (int x = 0; x < numberOfBullet; ++x)
        {
            yield return new WaitForSeconds(fireRate);
            Rigidbody bullet;
            bullet = Instantiate(m_bullet, transform.position, transform.rotation);
            bullet.AddForce(spawnPoint.forward * bulletSpeed, ForceMode.Impulse);
            bullet.GetComponent<Bullet>().SpawnBullet(bulletDamage);
            bullet.GetComponent<Bullet>().SetHitEffect(hitEffect);
        }
        yield return new WaitForSeconds(coolDown);
        canFire = true;
    }
}
