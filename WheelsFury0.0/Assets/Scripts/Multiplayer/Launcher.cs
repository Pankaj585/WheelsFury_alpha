using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Launcher : MonoBehaviourPunCallbacks
{
    MainMenuUIHandler UIHandler;

    private void Awake()
    {
        UIHandler = FindObjectOfType<MainMenuUIHandler>();
        PhotonNetwork.AutomaticallySyncScene = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Connecting to master...");
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to master");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Joined lobby");
        UIHandler.ShowLobby();
    }

    public void JoinRandomRoom()
    {
        PhotonNetwork.JoinRandomRoom();        
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Couldn't join room : " + message);
    }

    public override void OnJoinedRoom()
    {
        UIHandler.ShowWaitingScreen();
    }

    public void CreateRoom()
    {
       RoomOptions options = new RoomOptions();
        options.MaxPlayers = 4;
       PhotonNetwork.CreateRoom("RandomRoom" + Random.Range(0, 9999),options);       
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Couldn't create room : " + message);
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Disconnected : " + cause);
        UIHandler.ShowErrorScreen();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if(PhotonNetwork.PlayerList.Length == 4)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.LoadLevel(1);
            }
        }
    }
}
