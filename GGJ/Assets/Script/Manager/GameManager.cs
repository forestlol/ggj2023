using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    FireController player;

    [Space]

    [SerializeField]
    GameObject levelUpPanel;

    [Space(10)]

    [Header("List of Weapons")]
    [SerializeField]
    List<Weapon> weapons;

    public static GameManager instance;

    Dictionary<string, Weapon> weaponDict = new Dictionary<string, Weapon>();
    List<string> weaponNameList = new List<string>();
    List<Weapon> generatedWeapon = new List<Weapon>();
    int currentLevel;
    [SerializeField]
    int experience;
    [SerializeField]
    int expCap = 100;
    Weapon weapon;

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

    public void IncreaseEXP(int amount)
    {
        experience += ((amount * currentLevel) + (10 + currentLevel^2));

        if (experience > expCap)
            LevelUp();
    }

    void GiveWeapon(Weapon weapon)
    {
        player.TakeWeapon(weapon);
    }

    public Weapon GetWeaponType(string weaponName)
    {
        return weaponDict[weaponName];
    }

    void LevelUp()
    {
        expCap = expCap + (expCap * currentLevel) + (10 * currentLevel^2);
        GenerateWeaponForLevelUp();
        ++currentLevel;
        experience = 0;
        levelUpPanel.SetActive(true);
        Time.timeScale = 0;
    }

    void GenerateWeaponForLevelUp()
    {
        generatedWeapon.Clear();

        for (int i = 0; i < 3; ++i)
        {
            weapon = weaponDict[weaponNameList[UnityEngine.Random.Range(0, weaponNameList.Count)]];
            generatedWeapon.Add(weapon);
        }
    }

    public void SelectWeapon(int value)
    {
        levelUpPanel.SetActive(false);
        GiveWeapon(generatedWeapon[value]);
        Time.timeScale = 1;
    }

    public Weapon UpgradeWeapon(Weapon weapon)
    {
        Weapon temp = weapon;
        ++temp.weaponLevel;
        temp.bulletDamage += temp.bulletDamage;

        if (temp.weaponLevel % 2 == 0)
            temp.fireRate *= 0.9f;
        else if (temp.weaponLevel % 3 == 0)
        {
            ++temp.numberOfBullet;
            temp.coolDown *= 0.9f;
        }
        return temp;
    }
}

[Serializable]
public class Weapon
{
    [Header("Bullet Statistics")]
    public string weaponName;

    [Space(5)]

    public int weaponLevel;
    public GameObject weaponMesh;
    public Rigidbody bullet;

    [Space]

    public float fireRate;
    public int numberOfBullet;
    public int bulletDamage;
    public int bulletSpeed;

    [Space]
    public float coolDown;

    [Space]

    public GameObject hitEffect;

    public bool canFire;
}
