using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomItemScript : MonoBehaviour
{
    [HideInInspector] public string linkedRoom;
    
    public void JoinRoom()
    {
        LobbyScript.Instance.JoinRoomByName(linkedRoom);
    }
}
