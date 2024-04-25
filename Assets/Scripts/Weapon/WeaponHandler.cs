using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    private static WeaponHandler instance;
    public static WeaponHandler Instance { get { return instance; } }
    private void Awake() => instance = this;

    public enum WeaponHoldEnum
    {
        LightBow,
        Bow,
        HeavyBow,
    }

    private WeaponHoldEnum currentWeapon;

    public WeaponHoldEnum CurrentWeapon { get => currentWeapon; set => currentWeapon = value; }

}
