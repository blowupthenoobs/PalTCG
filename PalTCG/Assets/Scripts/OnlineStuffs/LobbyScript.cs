using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.UI;

public class LobbyScript : MonoBehaviourPunCallbacks
{
    [Header("UI")]
    public GameObject loadingGo;
    public Button createRoomButton;
    public Button startRoomButton;
    [Space]
    public Transform roomListParent;
    public GameObject roomListItemPrefab;



    private List<RoomInfo> currentRoomList = new List<RoomInfo>();
    private string customRoomName = "";
    
    private void Awake()
    {
        loadingGo.SetActive(true);
        createRoomButton.interactable = false;
    }

    private IEnumerator Start()
    {
        if(PhotonNetwork.InRoom)
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

    public override void OnRoomListUpdate(List<RoomInfo> roomListUpdate)
    {
        base.OnRoomListUpdate(roomListUpdate);

        if(currentRoomList.Count <= 0)
        {
            currentRoomList = roomListUpdate;

            loadingGo.SetActive(false);
            createRoomButton.interactable = true;
        }
        else
        {
            foreach(var room in roomListUpdate)
            {
                for(int i = 0; i < currentRoomList.Count; i++)
                {
                    if(currentRoomList[i].Name == room.Name)
                    {
                        List<RoomInfo> newList = currentRoomList;

                        if(room.RemovedFromList)
                            newList.Remove(newList[i]);
                        else
                            newList[i] = room;

                        currentRoomList = newList;
                    }
                }
            }
        }

        UpdateUI();
    }

    private void UpdateUI()
    {
        foreach(Transform roomItem in roomListParent)
        {
            Destroy(roomItem.gameObject);
        }

        foreach(var room in currentRoomList)
        {
            GameObject roomItem = Instantiate(roomListItemPrefab, roomListParent);

            roomItem.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = room.Name;
            roomItem.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = room.PlayerCount + "/2";
        }
    }

    public void ChangeHostedName(string text)
    {
        customRoomName = text;
        
        CheckForRoomName();
    }

    public void CheckForRoomName()
    {
        var fadeColor = startRoomButton.gameObject.GetComponent<Image>().color;
        
        if(customRoomName == "")
            startRoomButton.interactable = false;
        else
            startRoomButton.interactable = true;
        
    }

    public void CreateGame()
    {
        PlayerPrefs.SetString("roomnameToJoinOrCreate", customRoomName);

        SceneManager.LoadScene(1);
    }
}
