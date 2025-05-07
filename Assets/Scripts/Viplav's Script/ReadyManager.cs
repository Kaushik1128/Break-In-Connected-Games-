using Photon.Pun;
//using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ReadyManager : MonoBehaviourPunCallbacks
{
    private PhotonView photonView;
    int TotalPlayers;
    int readyPlayers = 0;
    public Dropdown[] roleDropdown;
    private string[] playerRoles;

    void Start()
    {
        photonView = GetComponent<PhotonView>();
        TotalPlayers = PhotonNetwork.PlayerList.Length;
        playerRoles = new string[TotalPlayers];
    }

    [PunRPC]
    public void SelectRoles()
    {
        for (int i = 0; i < roleDropdown.Length; i++)
        {
            string selectedRole = roleDropdown[i].options[roleDropdown[i].value].text;
            photonView.RPC("SyncRole", RpcTarget.AllBuffered, i, selectedRole);
        }
    }

    [PunRPC]
    public void SyncRole(int playerIndex, string role)
    {
        playerRoles[playerIndex] = role;
        Debug.Log($"Player {playerIndex} selected role: {role}");
    }

    [PunRPC]
    public void PlayerReady()
    {
        readyPlayers++;
        Debug.Log($"Player Ready {readyPlayers} / {TotalPlayers} players are ready");
        if (readyPlayers == TotalPlayers)
        {
            Debug.Log("All players are ready");
            photonView.RPC("StartGame", RpcTarget.AllBuffered);
        }
    }

    [PunRPC]
    public void StartGame()
    {
        for (int i = 0; i < playerRoles.Length; i++)
        {
            if (playerRoles[i] == "Hacker")
            {
                PhotonNetwork.LoadLevel("Hacker");
                return;
            }
        }
        PhotonNetwork.LoadLevel("Bank");
    }

    public void ClickReadyButton()
    {
        photonView.RPC("PlayerReady", RpcTarget.AllBuffered);
    }
}



