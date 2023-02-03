using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class FireController : MonoBehaviour
{
    [Header("Main Animator")]
    [SerializeField]
    Animator anim;

    [Header("Fire point")]
    [SerializeField]
    Transform spawnPoint;

    [Space]

    [Header("Current Weapon")]
    [SerializeField]
    Weapon currentWeapon;

    Rigidbody bullet;
    bool canFire = true;

    private void Start()
    {
        currentWeapon = GameManager.instance.GetWeaponType("Pistol");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            GameManager.instance.BuyWeapon(GameManager.instance.GetWeaponType("Rocket"));

        if (!Input.GetMouseButton(0))
            return;

        if (!canFire)
            return;

        StartCoroutine(Fire());
    }

    IEnumerator Fire()
    {
        canFire = false;
        bullet = Instantiate(currentWeapon.bullet, spawnPoint.position, spawnPoint.rotation);
        bullet.AddForce(spawnPoint.forward * currentWeapon.bulletSpeed, ForceMode.Impulse);
        bullet.GetComponent<Bullet>().SpawnBullet(currentWeapon.bulletDamage);
        bullet.GetComponent<Bullet>().SetHitEffect(currentWeapon.hitEffect);
        yield return new WaitForSeconds(currentWeapon.fireRate);
        canFire = true;
    }

    public void SetWeapon(Weapon weapon)
    {
        currentWeapon = weapon;
    }
}
