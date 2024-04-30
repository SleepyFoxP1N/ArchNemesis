using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    public enum WeaponHoldEnum
    {
        LightBow,
        Bow,
        HeavyBow,
    }

    [SerializeField] private WeaponHoldEnum currentWeaponEnum;

    [SerializeField] private GameObject currentWeapon;

    [SerializeField] private GameObject currentProjectile;

    public WeaponHoldEnum CurrentWeaponEnum { get => currentWeaponEnum; set => currentWeaponEnum = value; }
    public GameObject CurrentWeapon { get => currentWeapon; set => currentWeapon = value; }
    public GameObject CurrentProjectile { get => currentProjectile; set => currentProjectile = value; }
}
