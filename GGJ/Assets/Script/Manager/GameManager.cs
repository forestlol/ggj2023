using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using CartoonFX;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject levelUpPanel;
    [SerializeField]
    List<Button> gameButton;

    [Space(10)]

    [Header("List of Weapons")]
    [SerializeField]
    List<Weapon> weapons;

    [Space]

    [SerializeField]
    int experience;
    [SerializeField]
    int expCap = 100;

    [Space]
    [SerializeField]
    CFXR_Effect levelUpEffect;

    Weapon weapon;
    public static GameManager instance;

    Dictionary<string, Weapon> weaponDict = new Dictionary<string, Weapon>();
    List<string> weaponNameList = new List<string>();
    List<Weapon> generatedWeapon = new List<Weapon>();
    List<Weapon> currentWeapon = new List<Weapon>();
    int currentLevel;

    public FireController player;
    public PlayerMovement playerStats;

    public int room_size = 3, current_game_Level = 1, enemyHealthBonus = 0;

    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(this.gameObject);

        instance = this;

        AddWeaponToDictionary();
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Pushes all custom created weapon to the dictionary
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

    // Increase exp by an amount when enemy die
    public void IncreaseEXP(int amount)
    {
        amount = amount + currentLevel;
        experience += amount;

        if (experience > expCap)
            LevelUp();
    }


    // Return a weapon class from the dictionary
    // Used to find specific weapon
    public Weapon GetWeaponType(string weaponName)
    {
        return weaponDict[weaponName];
    }

    // Open the weapon selection screen once player level up
    // Temporary pause the game to give user time to choose
    // Reset experience and increase exp Cap
    void LevelUp()
    {
        expCap = Mathf.CeilToInt(expCap * 1.5f);
        GenerateWeaponForLevelUp();
        ++currentLevel;
        experience = 0;
        levelUpPanel.SetActive(true);
        Time.timeScale = 0;
    }

    // Randomly pull 3 weapon from Weapon Dict and add them to the generatedWeapon list
    // List is used for the button to faster find the weapon
    void GenerateWeaponForLevelUp()
    {
        generatedWeapon.Clear();

        for (int i = 0; i < 3; ++i)
        {
            weapon = weaponDict[weaponNameList[UnityEngine.Random.Range(0, weaponNameList.Count)]];
            gameButton[i].GetComponentInChildren<TextMeshProUGUI>().text = weapon.weaponName;
            generatedWeapon.Add(weapon);
        }
    }

    // Added to the button
    // Return the weapon from the list
    // Give player the weapon
    public void SelectWeapon(int value)
    {
        levelUpPanel.SetActive(false);
        GiveWeapon(generatedWeapon[value]);
        Time.timeScale = 1;
        CFXR_Effect temp = Instantiate(levelUpEffect, player.transform.position + new Vector3(0,.5f,0), Quaternion.identity);
    }

    //Self explanatory
    void GiveWeapon(Weapon weapon)
    {
        player.TakeWeapon(weapon);
    }

    // Activate once the player receive the weapon
    // Check whether weapon is existing in the player current dict
    // if it exist, upgrade the weapon
    public Weapon UpgradeWeapon(Weapon weapon)
    {
        Weapon temp = weapon;
        ++weapon.weaponLevel;
        temp.bulletDamage += temp.bulletDamage;

        if (temp.weaponName.Contains("upgrade"))
            return temp;

        if (temp.weaponLevel % 2 == 0)
            temp.fireRate *= 0.9f;
        else if (temp.weaponLevel % 3 == 0)
        {
            ++temp.numberOfBullet;
            temp.coolDown *= 0.9f;
        }
        return temp;
    }

    public void Upgradehealth(int value)
    {
        playerStats.IncreaseHealth(value);
    }
    public void UpgradeSpeed(int value)
    {
        playerStats.IncreaseSpeed(value);
    }

    public Transform GetPlayerPosition()
    {
        return player.transform;
    }

    public int GetUserWeaponsCount()
    {
        return currentWeapon.Count;
    } 

    public void SetUserWeapons()
    {
        currentWeapon = player.GetCurrentWeapon();
    }

    public List<Weapon> GetUserWSeapons()
    {
        return currentWeapon;
    }

    public void Game_CompletRoom()
    {
        current_game_Level++;
        room_size++;
        enemyHealthBonus += 10;
    }

    public void Game_ResetGameStats()
    {
        current_game_Level = 1;
        room_size = 3;
        enemyHealthBonus = 0;
        currentWeapon.Clear();
    }
}

[Serializable]
public class Weapon
{
    [Header("Bullet Statistics")]
    public string weaponName;

    [Space(5)]

    public int weaponLevel;
    public Rigidbody bullet;

    [Space]

    [Header("The interval between each bullet")]
    public float fireRate;
    [Space]
    public int numberOfBullet;
    public int bulletDamage;
    public int bulletSpeed;

    [Space]

    [Header("How long before the next fire")]
    public float coolDown;

    [Space]

    public GameObject hitEffect;
    [Space]

    public Gun gun;
    [Space]
    public string soundID;
}

