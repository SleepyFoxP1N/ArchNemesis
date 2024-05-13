using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.UIElements;

public class RoomList : MonoBehaviourPunCallbacks
{
    public static RoomList instance;


    public GameObject roomManager_GameObject;
    public RoomManager roomManager_Script;


    [Header("UI")]
    [SerializeField] Transform roomListParent;
    [SerializeField] GameObject roomListItemPrefab;



    // ===== ===== ===== ===== ===== ROOM VARIABLES
    private List<RoomInfo> cachedRoomList = new List<RoomInfo>();


    private void Awake()
    {
        instance = this;
    }


    IEnumerator Start()
    {
        // Precautions
        if (PhotonNetwork.InRoom)
        {
            PhotonNetwork.LeaveRoom();
            PhotonNetwork.Disconnect();
        }

        yield return new WaitUntil(() => !PhotonNetwork.IsConnected);

        PhotonNetwork.ConnectUsingSettings();
    }


    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();

        PhotonNetwork.JoinLobby();
    }


    public override void OnRoomListUpdate(List<RoomInfo> _roomList)
    {
        if (cachedRoomList.Count <= 0)
        {
            cachedRoomList = _roomList;
        }
        else
        {
            foreach (var room in _roomList)
            {
                for (int i = 0; i < cachedRoomList.Count; i++)
                {
                    if (cachedRoomList[i].Name == room.Name)
                    {
                        List<RoomInfo> newList = cachedRoomList;

                        if (room.RemovedFromList)
                        {
                            newList.Remove(newList[i]);
                        }
                        else
                        {
                            newList[i] = room;
                        }

                        cachedRoomList = newList;
                    }
                }
            }
        }
        UpdateUI();
    }


    void UpdateUI() 
    {
        foreach (Transform roomItem in roomListParent)
        {
            Destroy(roomItem.gameObject);
        }

        foreach (var room in cachedRoomList)
        {
            GameObject roomItem = Instantiate(roomListItemPrefab, roomListParent);

            roomItem.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = room.Name;
            roomItem.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = room.PlayerCount + "/7";

            roomItem.GetComponent<RoomItemButton>().RoomName = room.Name;
            roomItem.GetComponent<RoomItemButton>().RoomPlayerCount = room.PlayerCount;
        }
    }


    public void JoinRoomByName(string _name)
    {
        roomManager_Script.roomNameToJoin = _name;
        roomManager_GameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    
    public void ChangeRoomToCreateName(string _roomName)
    {
        roomManager_Script.roomNameToJoin = _roomName;
    }
}
