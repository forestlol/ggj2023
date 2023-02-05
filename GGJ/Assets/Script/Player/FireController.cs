using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        GameManager.instance.player = this;
        //Can take in a string value to change the default starting weapon
        int count = GameManager.instance.GetUserWeaponsCount();
        if(count == 0)
            TakeWeapon(GameManager.instance.GetWeaponType("Rocket"));
        else
        {
            currentWeapon = GameManager.instance.GetUserWSeapons();
            EquipSavedWeapons();
        }
    }

    void EquipSavedWeapons()
    {
        for (int i = 0; i < currentWeapon.Count; ++i)
        {
            AudioManager.instance.PlaySound("equip", transform);

            currentWeapon.Add(currentWeapon[i]);
            currentWeaponDict[currentWeapon[i].weaponName] = currentWeapon[i]; //This is for upgrading purpose, faster query

            gun = Instantiate(currentWeapon[i].gun, spawnPoint.transform); //Create a new gunObj and store at the firePoint
            gun.AddStats(currentWeapon[i], spawnPoint); // Parse in the stat from weapon

            gunDict[currentWeapon[i]] = gun;
        }
    }

    // Take the weapon from the Game Manager pool
    public void TakeWeapon(Weapon weapon)
    {
        //Check the List<Weapon> to find whether or not it is a new weapon
        //If it does not exist, add a new entry into the currentWeaponDict, dunDict
        if (!currentWeapon.Contains(weapon))
        {
            AudioManager.instance.PlaySound("equip", transform);

            currentWeapon.Add(weapon);
            currentWeaponDict[weapon.weaponName] = weapon; //This is for upgrading purpose, faster query

            gun = Instantiate(weapon.gun, spawnPoint.transform); //Create a new gunObj and store at the firePoint
            gun.AddStats(weapon, spawnPoint); // Parse in the stat from weapon
            guns.Add(gun);
            gunDict[weapon] = gun; // Reference <weapon,gun> for upgrade purpose

            if(weapon.weaponName.Contains("upgrade"))
                GameManager.instance.UpgradeWeapon(weapon);
            return;
        }
        // This is where upgrade occurs
        // Get query the weapon class that needs to be upgraded and GameManager UpgraWeapon will handle the upgrade
        currentWeaponDict[weapon.weaponName] = GameManager.instance.UpgradeWeapon(weapon);

        //Find the gun oject with via the weapon and apply the new stats
        gunDict[currentWeaponDict[weapon.weaponName]].AddStats(currentWeaponDict[weapon.weaponName], spawnPoint);

    }

    public List<Weapon> GetCurrentWeapon()
    {
        return currentWeapon;
    }
}
