using UnityEngine;
using Photon.Pun;
using System;
using Photon.Pun.Demo.Asteroids;

public class ManageSFX : MonoBehaviour
{
    [Header("WEAPON")]
    [SerializeField] AudioClip Fire_CLIP;
    [SerializeField] AudioClip Reload_CLIP;

    // ===== ===== ===== ===== ===== EVENTS
    public Action onWeaponFIRE;
    public Action onWeaponRELOAD;
    public Action onBulletIMPACT;

    // ===== ===== ===== ===== ===== SCRIPT
    private Weapon Weapon_SCRIPT;

    public void setWeaponSFX(Weapon _weapon)
    {
        Weapon_SCRIPT = _weapon;
        //_weapon.onFIRE += playWeaponFireSFX;
        //_weapon.onRELOAD += playWeaponReloadSFX;
    }


    //————————————————————————————————————————————————————————————————————————————//
    //                            WEAPON METHODS                                  //      
    //————————————————————————————————————————————————————————————————————————————//

    public void playWeaponFireSFX()
    {
        //Weapon_SCRIPT.playSound(Fire_CLIP);
    }
    public void playWeaponReloadSFX()
    {
        //Weapon_SCRIPT.playSound(Reload_CLIP);
    }


    //————————————————————————————————————————————————————————————————————————————//
    //                            WEAPON RPC EVENTS                               //      
    //————————————————————————————————————————————————————————————————————————————//
    
    [PunRPC]
    public void WeaponFireSFX_RPC()
    {
        onWeaponFIRE?.Invoke();
    }

    [PunRPC]
    public void WeaponReloadSFX_RPC()
    {
        onWeaponRELOAD?.Invoke();
    }


}
