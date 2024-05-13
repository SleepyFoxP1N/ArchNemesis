using UnityEngine;
using Photon.Pun;
using System;
using Photon.Pun.Demo.Asteroids;

public class ManagerSFX : MonoBehaviour
{
    [Header("Weapon")]
    [SerializeField] AudioClip Shoot_CLIP;
    [SerializeField] AudioClip Reload_CLIP;
    [SerializeField] AudioClip Walk_CLIP;

    // ===== ===== ===== ===== ===== SCRIPT
    [Header("Scripts")]
    public WeaponBehavior weaponBehavior;
    public PlayerController playerController;

    public void SetWeaponSFX(PlayerController _playerController, WeaponBehavior _weaponBehavior)
    {
        weaponBehavior = _weaponBehavior;
        playerController = _playerController;
        playerController.onWeaponShoot += playWeaponFireSFX;
        playerController.onWeaponReload += playWeaponReloadSFX;
        playerController.onWalk += playWalkSFX;
    }


    //————————————————————————————————————————————————————————————————————————————//
    //                            WEAPON METHODS                                  //      
    //————————————————————————————————————————————————————————————————————————————//

    public void playWeaponFireSFX(int projectile)
    {
        weaponBehavior.playSound(Shoot_CLIP);
    }
    public void playWeaponReloadSFX(int projectile, int bag)
    {
        weaponBehavior.playSound(Reload_CLIP);
    }
    public void playWalkSFX(bool walking)
    {
        if(walking) playerController.playSound(Walk_CLIP);
    }
}
