using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    FireController player;

    [Space(10)]

    [Header("List of Weapons")]
    [SerializeField]
    List<Weapon> weapons;

    int cash;

    public static GameManager instance;

    Dictionary<string, Weapon> weaponDict = new Dictionary<string, Weapon>();
    List<string> weaponNameList = new List<string>();

    // To do create a shop system 
    // Generate 3 random weapon from the list of string (List<string> weaponNameList)
    // Add BuyWeapon method to the button 

    private void Awake()
    {
        instance = this;

        AddWeaponToDictionary();
    }

    void AddWeaponToDictionary()
    {
        string name = "";
        for (int i = 0; i < weapons.Count; ++i)
        {
            weaponDict[weapons[i].weaponName] = weapons[i];
            name = weapons[i].weaponName;
            weaponNameList.Add(name);
        }
    }

    public void IncreaseCase(int amount)
    {
        cash += amount;
    }

    public void BuyWeapon(Weapon weapon)
    {
        // not enough cash
        if (cash - weapon.cost < 0)
            return;
        
        cash -= weapon.cost;
        GiveWeapon(weapon);
    }

    void GiveWeapon(Weapon weapon)
    {
        player.SetWeapon(weapon);
    }

    public Weapon GetWeaponType(string weaponName)
    {
        return weaponDict[weaponName];
    }
}

[Serializable]
public class Weapon
{
    [Header("Bullet Statistics")]
    public string weaponName;

    [Space(5)]

    public GameObject weaponMesh;
    public Rigidbody bullet;
    [Range(0.05f, 3f)]
    public float fireRate;
    [Range(1, 10)]
    public int bulletDamage;
    [Range(10, 100)]
    public int bulletSpeed;
    public int cost;

    [Space]

    public GameObject hitEffect;

    [Space]
    public AnimatorController controller;

}
