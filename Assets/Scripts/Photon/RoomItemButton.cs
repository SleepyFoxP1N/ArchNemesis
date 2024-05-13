using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomItemButton : MonoBehaviour
{
    public string RoomName;
    public int RoomPlayerCount;
    public void OnButtonPressed()
    {
        if(RoomPlayerCount < 7)
        {
            RoomList.instance.JoinRoomByName(RoomName);
        }
    }
}
