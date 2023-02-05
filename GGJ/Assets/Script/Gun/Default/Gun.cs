using CartoonFX;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [HideInInspector]
    public int bulletDamage;
    [HideInInspector]
    public int numberOfBullet;
    [HideInInspector]
    public int bulletSpeed;
    [HideInInspector]
    public float fireRate;
    [HideInInspector]
    public float coolDown;
    [HideInInspector]
    public Transform spawnPoint;
    [HideInInspector]
    public Rigidbody m_bullet;
    [HideInInspector]
    public GameObject hitEffect;
    [HideInInspector]
    public bool canFire;
    [HideInInspector]
    public int weaponLevel;
    [HideInInspector]
    public string soundID;

    [Header("Damage Effect")]
    public CFXR_ParticleText effect;

    //Initialize the Gun class
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
        this.weaponLevel = weapon.weaponLevel;
        this.soundID = weapon.soundID;
    }

    // Update is called once per frame
    // Set to auto fire
    void Update()
    {
        if (!canFire)
            return;

        StartCoroutine(Fire());
    }

    public virtual IEnumerator Fire()
    {
        canFire = false;
        for (int x = 0; x < numberOfBullet; ++x)
        {
            //To do: Make a sphere cast to get array of GameObject[]
            // Get cloest enemy, fire towards that dir
            // If there is no enemy, fire on the players forward (spawnPoint)
            yield return new WaitForSeconds(fireRate);

            AudioManager.instance.PlaySound(soundID, transform);
            Rigidbody bullet;
            bullet = Instantiate(m_bullet, transform.position, transform.rotation);
            bullet.AddForce(spawnPoint.forward * bulletSpeed, ForceMode.Impulse);
            bullet.GetComponent<Bullet>().SetDamagetext(effect);
            bullet.GetComponent<Bullet>().SpawnBullet(bulletDamage);
            bullet.GetComponent<Bullet>().SetHitEffect(hitEffect);
        }
        yield return new WaitForSeconds(coolDown);

        canFire = true;
    }
}
