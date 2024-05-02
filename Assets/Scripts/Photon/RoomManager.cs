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

    [Header("UI")]
    [SerializeField] GameObject name_UI;
    [SerializeField] GameObject connecting_UI;


    [Header("MANAGERS")]
    [SerializeField] ManageSFX ManageSFX_SCRIPT;


    // ===== ===== ===== ===== ===== ROOM VARIABLE
    [HideInInspector]
    public string roomNameToJoin = "test";



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

        Debug.Log("Fired when client is joined");
        roomCamera.SetActive(false);

        SpawnPlayer();
    }


    public void SpawnPlayer() 
    { 
        Transform spawnPoint = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)];

        GameObject player = PhotonNetwork.Instantiate(Player_OBJ.name, spawnPoint.position, Quaternion.identity);

        player.GetComponent<PlayerSetup>().IsLocalPlayer();
        player.GetComponent<Health>().isLocalPlayer = true;
        player.GetComponent<PlayerAim>().isLocalPlayer = true;
        player.GetComponent<PhotonView>().RPC("ChangeNickname", RpcTarget.AllBuffered, nickname);

        //setWeapon(player, "arms@ak 1", 1);
        //setWeapon(player, "arms@ak 2", 2);

        PhotonNetwork.LocalPlayer.NickName = nickname;
    }

    public void setWeapon(GameObject player, string name, int num)
    {
        Weapon weapon = player.transform.Find("Eye Camera/Weapon Camera/Weapon Holder/"+ name).GetComponent<Weapon>();
        ManageSFX_SCRIPT.setWeaponSFX(weapon);
        //weapon.setSFX_RPC(ManageSFX_SCRIPT); <--- TODO:
    }


    public void ChangeNickname(string _name)
    {
        nickname = _name;
    }


    public void JoinRoomButtonPressed()
    {
        Debug.Log("Connecting...");

        
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
