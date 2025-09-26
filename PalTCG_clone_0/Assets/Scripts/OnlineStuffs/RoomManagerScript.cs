using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class RoomManagerScript : MonoBehaviourPunCallbacks
{
    //To spawn things over the network, it needs to be in a folder called Resources
    //and then use PhotonNetwork.Instantiate({prefabvariablename}.name, Vector3, rotation)
    public static RoomManagerScript Instance;
    [SerializeField] GameObject nameUI;
    [SerializeField] GameObject playerNamePlate;
    [SerializeField] GameObject enemyNamePlate;
    [SerializeField] GameObject waitingScreen;
    private bool opponentReady;
    private bool playerReady;

    private void Awake()
    {
        waitingScreen.SetActive(true);
        
        if(Instance != null)
            Destroy(gameObject);
        else
            Instance = this;
        
        if(!PhotonNetwork.IsConnectedAndReady)
            SceneManager.LoadScene(0);

        string roomNameToJoin = PlayerPrefs.GetString("roomnameToJoinOrCreate");

        PhotonNetwork.JoinOrCreateRoom(roomNameToJoin, null, null);
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();

        PhotonNetwork.LocalPlayer.NickName = AccountManager.Instance.player.accountName;
        // nameUI.SetActive(true);
        // enemyNamePlate
    }

    [PunRPC]
    public void ReadyForMatch()
    {
        waitingScreen.SetActive(false);
        HandFunctions.DrawCards(5);
    }

    public void PlayerLockedIn()
    {
        playerReady = true;
        PhotonView.Get(this).RPC("OpponentLockedIn", RpcTarget.OthersBuffered);

        if(playerReady && opponentReady)
        {
            PhotonView.Get(this).RPC("ReadyForMatch", RpcTarget.All);
            GameManager.Instance.PickFirstPlayer();
        }
        
    }

    [PunRPC]
    public void OpponentLockedIn()
    {
        opponentReady = true;
    }
}
