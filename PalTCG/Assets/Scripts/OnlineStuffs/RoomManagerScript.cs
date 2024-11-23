using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class RoomManagerScript : MonoBehaviourPunCallbacks
{
    public GameObject nameUI;
    public GameObject playerNamePlate;
    public GameObject enemyNamePlate;

    private void Awake()
    {
        if(!PhotonNetwork.IsConnectedAndReady)
            SceneManager.LoadScene(0);

        string roomNameToJoin = PlayerPrefs.GetString("roomnameToJoinOrCreate");

        PhotonNetwork.JoinOrCreateRoom(roomNameToJoin, null, null);
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();

        // nameUI.SetActive(true);
        // enemyNamePlate
    }
}
