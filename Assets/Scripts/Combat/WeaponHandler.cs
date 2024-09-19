using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    [SerializeField] private GameObject weaponLogic;
    [SerializeField] private GameObject secondaryWeaponLogic;

    public void EnableWeapon()
    {
        weaponLogic.SetActive(true);
    }

    public void DisableWeapon() 
    { 
        weaponLogic.SetActive(false); 
    }

    public void EnableSecondaryWeapon()
    {
        secondaryWeaponLogic.SetActive(true);
    }

    public void DisableSecondaryWeapon()
    {
        secondaryWeaponLogic.SetActive(false);
    }
}
