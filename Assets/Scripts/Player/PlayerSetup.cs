using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class PlayerSetup : MonoBehaviour
{


    [SerializeField] PlayerController playerController;
    [SerializeField] GameObject mainCam;
    [SerializeField] GameObject virtualCam;
    [SerializeField] TextMeshPro name_TextMesh;
    [SerializeField] Transform TPweaponHolder;



    [Header("PLAYER INFO")]
    [SerializeField] string nickname;


    public void IsLocalPlayer()
    {
        TPweaponHolder.gameObject.SetActive(false);
        playerController.enabled = true;
        mainCam.SetActive(true);
        virtualCam.SetActive(true);
    }


    [PunRPC]
    public void SetTPWeapon(int _weaponIndex)
    {
        foreach (Transform _weapon in TPweaponHolder)
        {
            _weapon.gameObject.SetActive(false);
        }

        TPweaponHolder.GetChild(_weaponIndex).gameObject.SetActive(true);
    }


    [PunRPC]
    public void ChangeNickname(string _name)
    {
        nickname = _name;
        //name_TextMesh.text = nickname;
    }
}
