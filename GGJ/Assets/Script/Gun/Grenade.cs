using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : Gun
{
    private void Update()
    {
        if (!canFire)
            return;

        StartCoroutine(Fire());
    }

    public override IEnumerator Fire()
    {
        canFire = false;
        for (int x = 0; x < numberOfBullet; ++x)
        {
            //To do: Make a sphere cast to get array of GameObject[]
            // Get cloest enemy, fire towards that dir
            // If there is no enemy, fire on the players forward (spawnPoint)
            yield return new WaitForSeconds(fireRate);
            Rigidbody bullet;
            bullet = Instantiate(m_bullet, transform.position, transform.rotation);
            bullet.AddForce((spawnPoint.forward * (bulletSpeed/2)) + (spawnPoint.up * (bulletSpeed/2)), ForceMode.Impulse);
            bullet.GetComponent<Bullet>().SpawnBullet(bulletDamage);
            bullet.GetComponent<Bullet>().SetHitEffect(hitEffect);
        }
        yield return new WaitForSeconds(coolDown);
        canFire = true;
    }
}
