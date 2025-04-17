using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.SceneManagement;

public class PhotonRoomManager : MonoBehaviourPunCallbacks
{
    public TMP_InputField roomNameInput;
    public TMP_Text feedbackText;

    private void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();
            ShowMessage("Connecting to Photon...");
        }
        else
        {
            ShowMessage("Connected to Photon!");
        }
    }

    public void OnCreateRoomButton()
    {
        string roomName = roomNameInput.text;
        if (!string.IsNullOrEmpty(roomName))
        {
            PhotonNetwork.CreateRoom(roomName, new RoomOptions { MaxPlayers = 4 });
        }
    }

    public void OnJoinRoomButton()
    {
        string roomName = roomNameInput.text;
        if (!string.IsNullOrEmpty(roomName))
        {
            PhotonNetwork.JoinRoom(roomName);
        }
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
        ShowMessage("Connected to Photon Master!");
    }

    public override void OnJoinedRoom()
    {
        ShowMessage("Joined room: " + PhotonNetwork.CurrentRoom.Name);
        SceneManager.LoadScene("Role Select Scene");
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        ShowMessage("Failed to join room: " + message);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        ShowMessage("Failed to create room: " + message);
    }

    void ShowMessage(string msg)
    {
        if (feedbackText != null)
            feedbackText.text = msg;
    }
}
