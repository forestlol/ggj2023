using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour
{
    enum WeaponType
    {
        MELEE,
        PISTOL,
        RIFLE,
        ROCKET
    };

    [Header("Select Weapon type")]
    [SerializeField]
    WeaponType weaponType;

    [Header("Fire point")]
    [SerializeField]
    Transform spawnPoint;

    [Header("Bullet Prefab")]
    [SerializeField]
    Rigidbody m_bullet;

    [Space]

    [Header("Current Weapon")]
    [SerializeField]
    Weapon currentWeapon;

    [Space(10)]

    [Header("List of Weapons")]
    [SerializeField]
    List<Weapon> weapons;

    Rigidbody bullet;
    bool canFire = true;

    Dictionary<string, Weapon> weaponDict = new Dictionary<string, Weapon>();

    private void Start()
    {
        AddWeaponToDictionary();
    }

    void AddWeaponToDictionary()
    {
        for (int i = 0; i < weapons.Count; ++i)
        {
            weaponDict[weapons[i].weaponName] = weapons[i];
        }

        currentWeapon = weaponDict[weapons[0].weaponName];
    }
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
        bullet.AddForce(spawnPoint.forward * currentWeapon.bulletSpeed, ForceMode.Impulse);
        bullet.GetComponent<Bullet>().SpawnBullet(currentWeapon.bulletDamage);

        yield return new WaitForSeconds(currentWeapon.fireRate);
        canFire = true;
    }

    public void GetWeaponType(string weaponName)
    {
        currentWeapon = weaponDict[weaponName];
    }
}

[Serializable]
public class Weapon
{
    [Header("Bullet Statistics")]
    public string weaponName;
    [Range(0.05f, 1f)]
    public float fireRate;
    [Range(1, 10)]
    public int bulletDamage;
    [Range(1, 10)]
    public int bulletSpeed;
}
