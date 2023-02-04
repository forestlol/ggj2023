using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : Gun
{
    int currentlevel = -1;

    private void Update()
    {
        GetPowerUp();
    }

    void GetPowerUp()
    {
        if(currentlevel == weaponLevel)
            return;

        switch (fireRate)
        {
            case 0:
                GameManager.instance.Upgradehealth(bulletDamage);
                break;
            case 1:
                GameManager.instance.UpgradeSpeed(bulletDamage);
                break;
        }

        ++ currentlevel;
    }
}
