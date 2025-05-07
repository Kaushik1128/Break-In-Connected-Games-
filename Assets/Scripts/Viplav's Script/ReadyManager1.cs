using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;

public class ReadyManager1 : MonoBehaviourPunCallbacks
{
    private PhotonView photonView;
    private int totalPlayers;
    private int readyPlayers = 0;
    public Dropdown roleDropdown;
    private Dictionary<int, string> playerRoles = new Dictionary<int, string>(); // Store roles by PlayerID
    public Text welcome;

    void Start()
    {
        welcome.text = "Welcome " + PhotonNetwork.LocalPlayer.NickName;
        photonView = GetComponent<PhotonView>();
        totalPlayers = PhotonNetwork.PlayerList.Length;
    }

    public void SelectRole()
    {
        string selectedRole = roleDropdown.options[roleDropdown.value].text;
        int playerId = PhotonNetwork.LocalPlayer.ActorNumber; // Unique player ID

        // Send role selection to all players
        photonView.RPC("SyncRole", RpcTarget.AllBuffered, playerId, selectedRole);
    }

    [PunRPC]
    public void SyncRole(int playerId, string role)
    {
        if (!playerRoles.ContainsKey(playerId))
        {
            playerRoles[playerId] = role;
            Debug.Log($"Player {playerId} selected role: {role}");
        }
        UpdateDropdownOptions();
    }

    private void UpdateDropdownOptions()
    {
        // Get all selected roles
        HashSet<string> selectedRoles = new HashSet<string>(playerRoles.Values);

        // Filter dropdown options
        List<Dropdown.OptionData> updatedOptions = new List<Dropdown.OptionData>();
        foreach (Dropdown.OptionData option in roleDropdown.options)
        {
            if (!selectedRoles.Contains(option.text)) // Keep only unselected roles
            {
                updatedOptions.Add(option);
            }
        }

        roleDropdown.options = updatedOptions;
        roleDropdown.RefreshShownValue();
    }

    [PunRPC]
    public void PlayerReady()
    {
        readyPlayers++;
        Debug.Log($"Player Ready {readyPlayers} / {totalPlayers} players are ready");
        if (readyPlayers == 4)
        {
            Debug.Log("All players are ready");
            photonView.RPC("StartGame", RpcTarget.AllBuffered);
        }
    }

    [PunRPC]
    public void StartGame()
    {
        if (PhotonNetwork.IsMasterClient)
        {
        
            PhotonNetwork.LoadLevel("Test");
        }
    }


    public void ClickReadyButton()
    {
        photonView.RPC("PlayerReady", RpcTarget.AllBuffered);
    }
}
