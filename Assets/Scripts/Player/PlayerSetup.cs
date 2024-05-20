using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class PlayerSetup : MonoBehaviour
{
    [Header("GameObjects")]
    [SerializeField] GameObject mainCam;
    [SerializeField] GameObject virtualCam;
    [SerializeField] GameObject weaponHolder;
    [SerializeField] GameObject weapon;
    [SerializeField] GameObject nameTag;


    [Header("PLAYER INFO")]
    [SerializeField] string nickname;


    public void IsLocalPlayer()
    {
        gameObject.GetComponent<PlayerAim>().enabled = true;
        gameObject.GetComponent<Zoom>().enabled = true;
        gameObject.GetComponent<Player>().enabled = true;
        gameObject.GetComponent<PlayerController>().enabled = true;
        gameObject.GetComponent<PlayerShoot>().enabled = true;
        gameObject.AddComponent<AudioListener>();

        weapon.GetComponent<WeaponBehavior>().enabled = true;
        mainCam.SetActive(true);
        virtualCam.SetActive(true);
    }

    [PunRPC]
    public void SetTPWeapon(int _weaponIndex)
    {
        Transform weaponHolder_T = weaponHolder.transform;
        foreach (Transform _weapon in weaponHolder_T)
        {
            _weapon.gameObject.SetActive(false);
        }

        weaponHolder_T.GetChild(_weaponIndex).gameObject.SetActive(true);
    }


    [PunRPC]
    public void ChangeNickname(string _name)
    {
        nickname = _name;
        nameTag.GetComponent<TextMeshPro>().text = nickname;
    }
}
