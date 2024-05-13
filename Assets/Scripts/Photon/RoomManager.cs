using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public static RoomManager instance;

    [Space]
    [SerializeField] GameObject Player_OBJ;
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] GameObject roomCamera;
    [SerializeField] string hostPlayerID;

    [Header("UI")]
    [SerializeField] GameObject name_UI;
    [SerializeField] GameObject connecting_UI;


    [Header("MANAGERS")]
    [SerializeField] ManagerSFX managerSFX;
    [SerializeField] WeaponSpawner weaponSpawner;
    [SerializeField] TimerUI timerUI;


    // ===== ===== ===== ===== ===== ROOM VARIABLE
    [HideInInspector]
    public string roomNameToJoin = "Room";



    // ===== ===== ===== ===== ===== PLAYER VARIABLE
    private string nickname = "unnamed";
    [HideInInspector]
    public int kills = 0;
    [HideInInspector]
    public int deaths = 0;


    void Awake()
    {
        instance = this;
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();

        roomCamera.SetActive(false);
        GameObject player = SpawnPlayer();
        SetHostPlayer(player);
    }

    private void SetHostPlayer(GameObject player)
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            hostPlayerID = PhotonNetwork.LocalPlayer.UserId;
        }
        if (PhotonNetwork.IsMasterClient && hostPlayerID == PhotonNetwork.LocalPlayer.UserId)
        {
            StartCoroutine(weaponSpawner.SpawnWeapons());
            StartCoroutine(timerUI.timer(player));
        }
    }


    public GameObject SpawnPlayer() 
    { 
        Transform spawnPoint = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)];

        GameObject player = PhotonNetwork.Instantiate(Player_OBJ.name, spawnPoint.position, Quaternion.identity);
        
        setWeaponSFX(player);
        player.GetComponent<PlayerSetup>().IsLocalPlayer();
        player.GetComponent<Health>().isLocalPlayer = true;
        player.GetComponent<PlayerAim>().isLocalPlayer = true;
        player.GetComponent<PhotonView>().RPC("ChangeNickname", RpcTarget.AllBuffered, nickname);

        PhotonNetwork.LocalPlayer.NickName = nickname;
        return player;
    }

    private void setWeaponSFX(GameObject player)
    {
        managerSFX.SetWeaponSFX(player.GetComponent<PlayerController>(), player.transform.Find("Weapon Holder/Rotate Point/Weapon").GetComponent<WeaponBehavior>());
    }


    public void ChangeNickname(string _name)
    {
        nickname = _name;
    }


    public void JoinRoomButtonPressed()
    {
        Debug.Log("Connecting...");

        if (!PhotonNetwork.IsConnectedAndReady)
        {
            Debug.LogError("Not connected to the Master Server.");
            return;
        }

        PhotonNetwork.JoinOrCreateRoom(roomNameToJoin, null, null);

        name_UI.SetActive(false);
        connecting_UI.SetActive(true);
    }



    public void SetHashes()
    {
        try
        {
            Hashtable hash = PhotonNetwork.LocalPlayer.CustomProperties;

            hash["Kills"] = kills;
            hash["Deaths"] = deaths;

            PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
        }
        catch
        {
            // Do nothing here
        }
    }

}
