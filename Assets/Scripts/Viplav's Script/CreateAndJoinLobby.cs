using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class CreateAndJoinLobby : MonoBehaviourPunCallbacks
{
    public InputField CreateInput;
    public InputField JoinInput;


    private void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        Debug.Log("Photon network status Login : " + PhotonNetwork.NetworkClientState);
        if (!PhotonNetwork.IsConnected)
        {
            Debug.Log("Scene disconnected, reconnecting");
            PhotonNetwork.ConnectUsingSettings();
        }
    }
    // this function is when you create a room
    public void CreateRoom()
    {
        //Create Room function is created which needs a string paramatere to be entered 
        //if (PhotonNetwork.IsConnected && PhotonNetwork.NetworkClientState == ClientState.ConnectedToMasterServer)
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 4;

            PhotonNetwork.CreateRoom(CreateInput.text,roomOptions,TypedLobby.Default);
        
    }
    public override void OnCreatedRoom()
    {
        PhotonNetwork.LoadLevel("Lobby_2");
    }
    // this function is when you join a room
    public void JoinRoom()
    {
        //Join Room function is created which needs a string paramatere to be entered 
        //if (PhotonNetwork.IsConnected && PhotonNetwork.NetworkClientState == ClientState.ConnectedToMasterServer)
        
            PhotonNetwork.JoinRoom(JoinInput.text);
        
    }
    
    public override void OnJoinedRoom()
    {
        // when you create a room you automatically join it this is how you load level in Photon this will lead to game 
        //PhotonNetwork.LoadLevel("Lobby_2");
        Debug.Log("Joined Room");
    }
}

