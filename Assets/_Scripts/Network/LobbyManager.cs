using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

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
    }

    public void StopMatchSearch()
    {
        MainMenuPanel.SetActive(true);
        QuickMatchPanel.SetActive(false);
    }
}
