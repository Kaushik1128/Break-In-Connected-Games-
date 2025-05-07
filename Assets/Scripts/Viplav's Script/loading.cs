using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

// a callback function is a function that gets called automatically by photon when certain events happen
public class loading : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    //Connecting to Photon Server inside start function
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    // callback function 
    public override void OnConnectedToMaster()
    {
        // power to create and join rooms later on
        PhotonNetwork.JoinLobby();
    }
    // another callback function once joined the lobby go to the "lobby" scene
    public override void OnJoinedLobby()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
