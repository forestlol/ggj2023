using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor.Animations;
using UnityEngine;

public class FireController : MonoBehaviour
{
    [Header("Gun object")]
    [SerializeField]
    Gun gunObj;

    [Header("Main Animator")]
    [SerializeField]
    Animator anim;

    [Header("Fire point")]
    [SerializeField]
    Transform spawnPoint;

    [Space]

    [Header("Current Weapon Equipped")]
    [SerializeField]
    List<Weapon> currentWeapon;
    [SerializeField]
    List<Gun> guns;

    Dictionary <string, Weapon> currentWeaponDict = new Dictionary<string, Weapon>();
    Dictionary<Weapon, Gun> gunDict = new Dictionary<Weapon, Gun>();
    Gun gun;

    private void Start()
    {
        //currentWeapon.Add(GameManager.instance.GetWeaponType("Pistol"));
        TakeWeapon(GameManager.instance.GetWeaponType("Pistol"));
    }

    public void TakeWeapon(Weapon weapon)
    {
        if (!currentWeapon.Contains(weapon))
        {
            currentWeapon.Add(weapon);
            currentWeaponDict[weapon.weaponName] = weapon;

            gun = Instantiate(gunObj, spawnPoint.transform);
            gun.AddStats(weapon, spawnPoint);
            guns.Add(gun);
            gunDict[weapon] = gun;

            return;
        }

        currentWeaponDict[weapon.weaponName] = GameManager.instance.UpgradeWeapon(weapon);
        gunDict[currentWeaponDict[weapon.weaponName]].AddStats(currentWeaponDict[weapon.weaponName], spawnPoint);

    }
}
