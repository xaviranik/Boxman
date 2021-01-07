using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using System;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject MainMenuPanel;
    [SerializeField] private GameObject QuickMatchPanel;
    [SerializeField] private GameObject LoadingPanel;
    [SerializeField] private TMP_Text LoadingText;

    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();

        MainMenuPanel.SetActive(false);
        QuickMatchPanel.SetActive(false);
        LoadingPanel.SetActive(true);
        LoadingText.SetText("Connecting to server...");
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        Debug.Log("Connected to master server.");
        PhotonNetwork.AutomaticallySyncScene = true;

        LoadingPanel.SetActive(false);
        MainMenuPanel.SetActive(true);
        LoadingText.SetText("Connected to server");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void QuickMatch()
    {
        MainMenuPanel.SetActive(false);
        QuickMatchPanel.SetActive(true);

        PhotonNetwork.JoinRandomRoom();
    }

    public void StopMatchSearch()
    {
        MainMenuPanel.SetActive(true);
        QuickMatchPanel.SetActive(false);

        if(PhotonNetwork.InRoom)
        {
            PhotonNetwork.LeaveRoom();
        }
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        base.OnJoinRandomFailed(returnCode, message);
        MakeRoom();
    }

    private void MakeRoom()
    {
        int roomID = UnityEngine.Random.Range(1000, 9999);
        RoomOptions roomOptions = new RoomOptions() { IsVisible=true, IsOpen=true, MaxPlayers=6 };
        PhotonNetwork.CreateRoom("R" + roomID, roomOptions);

        Debug.Log("Room created! Waiting for other players.");
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        if(PhotonNetwork.CurrentRoom.PlayerCount == 2 && PhotonNetwork.IsMasterClient)
        {
            Debug.Log("Starting game");
            PhotonNetwork.LoadLevel("Lobby");
        }
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);
        Debug.Log("Left the Room");
    }
}
