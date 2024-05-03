using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Unity.VisualScripting;

public class WeaponSpawnCollider : MonoBehaviour
{
    private GameObject circleVFX;
    private GameObject playerWeapon;
    private GameObject weaponSpawned;

    private void Start()
    {
        circleVFX = gameObject.transform.parent.Find("circleVFX").gameObject;
    }

    private void OnTouch(Collider2D other)
    {
        playerWeapon = other.transform.Find("Weapon Holder/Rotate Point/Weapon").gameObject;

        if (playerWeapon == null) return;

        weaponSpawned = gameObject.transform.parent.Find("Weapon(Clone)").gameObject;

        playerWeapon.GetComponent<WeaponBehavior>().currentWeapon_Obj = weaponSpawned.GetComponent<WeaponBehavior>().currentWeapon_Obj;
        playerWeapon.GetComponent<WeaponBehavior>().UpdateSprite();
        circleVFX.SetActive(false);
        PhotonNetwork.Destroy(weaponSpawned);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        OnTouch(other);
    }

}
