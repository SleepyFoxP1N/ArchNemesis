using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Unity.VisualScripting;
using System;

public class WeaponSpawnCollider : MonoBehaviour
{
    public GameObject WeaponSpawned; // Initialized in weaponSpawner
    public GameObject circleVFX;
    private Weapon weaponObj;

    [PunRPC]
    private void OnTouchWeaponSpawner(int playerId)
    {
        GameObject player = PhotonView.Find(playerId).gameObject;
        GameObject playerWeapon = player.transform.Find("Weapon Holder/Rotate Point/Weapon").gameObject;
        GameObject weaponUI = player.transform.Find("Main Camera/Canvas/WeaponUI").gameObject;

        if (playerWeapon == null) return;

        weaponObj = WeaponSpawned.GetComponent<WeaponBehavior>().CurrentWeapon_Obj;

        playerWeapon.GetComponent<WeaponBehavior>().CurrentWeapon_Obj = weaponObj;
        player.GetComponent<PlayerShoot>().SetUp(weaponObj);
        weaponUI.GetComponent<WeaponUI>().InitializeScript(weaponObj);

        playerWeapon.GetComponent<PhotonView>().RPC("UpdateWeaponRPC", RpcTarget.AllBuffered, Array.IndexOf(playerWeapon.GetComponent<WeaponBehavior>().weapons, weaponObj));
        
        circleVFX.SetActive(false);

        if (GetComponent<PhotonView>().IsMine)
        {
            PhotonNetwork.Destroy(WeaponSpawned);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Player>() != null && WeaponSpawned != null)
        {
            GetComponent<PhotonView>().RPC("OnTouchWeaponSpawner", RpcTarget.AllBuffered, other.gameObject.GetPhotonView().ViewID);
        }
    }

}
