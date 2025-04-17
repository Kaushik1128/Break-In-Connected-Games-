using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class RoleSelectionManager : MonoBehaviourPunCallbacks
{
    public Button mainRobberButton, lockPickerButton, insiderButton, hackerButton;
    public TMP_Text feedbackText;
    public Button startGameButton;

    private Dictionary<string, Button> roleButtons;

    private void Start()
    {
        // Assign buttons to roles
        roleButtons = new Dictionary<string, Button>
        {
            { "MainRobber", mainRobberButton },
            { "LockPicker", lockPickerButton },
            { "Insider", insiderButton },
            { "Hacker", hackerButton }
        };

        // Sync roles for all players
        UpdateRoleButtons();

        // Hook up buttons
        mainRobberButton.onClick.AddListener(() => ToggleRole("MainRobber"));
        lockPickerButton.onClick.AddListener(() => ToggleRole("LockPicker"));
        insiderButton.onClick.AddListener(() => ToggleRole("Insider"));
        hackerButton.onClick.AddListener(() => ToggleRole("Hacker"));
    }

    void ToggleRole(string role)
    {
        string currentRole = GetMySelectedRole();

        if (currentRole == role)
        {
            PhotonNetwork.LocalPlayer.CustomProperties.Remove("SelectedRole");
        }
        else if (!IsRoleTaken(role))
        {
            Hashtable props = new Hashtable { { "SelectedRole", role } };
            PhotonNetwork.LocalPlayer.SetCustomProperties(props);
        }

        UpdateRoleButtons();
    }

    bool IsRoleTaken(string role)
    {
        foreach (Player p in PhotonNetwork.PlayerList)
        {
            if (p.CustomProperties.ContainsKey("SelectedRole") &&
                p.CustomProperties["SelectedRole"].ToString() == role)
                return true;
        }
        return false;
    }

    string GetMySelectedRole()
    {
        if (PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("SelectedRole"))
        {
            return PhotonNetwork.LocalPlayer.CustomProperties["SelectedRole"].ToString();
        }
        return null;
    }

    void UpdateRoleButtons()
    {
        foreach (var kvp in roleButtons)
        {
            string role = kvp.Key;
            Button button = kvp.Value;

            if (IsRoleTaken(role))
            {
                if (GetMySelectedRole() == role)
                {
                    SetButtonState(button, $"✓ {role} (You)", Color.cyan, true);
                }
                else
                {
                    SetButtonState(button, $"X {role} (Taken)", Color.gray, false);
                }
            }
            else
            {
                SetButtonState(button, $"● {role} (Available)", Color.green, true);
            }
        }

        startGameButton.interactable = AllRolesSelected() && PhotonNetwork.IsMasterClient;
    }

    void SetButtonState(Button button, string text, Color color, bool interactable)
    {
        button.GetComponentInChildren<TMP_Text>().text = text;
        ColorBlock cb = button.colors;
        cb.normalColor = color;
        cb.selectedColor = color;
        button.colors = cb;
        button.interactable = interactable;
    }

    bool AllRolesSelected()
    {
        HashSet<string> selectedRoles = new HashSet<string>();
        foreach (Player p in PhotonNetwork.PlayerList)
        {
            if (p.CustomProperties.ContainsKey("SelectedRole"))
                selectedRoles.Add(p.CustomProperties["SelectedRole"].ToString());
        }
        return selectedRoles.Count == 4;
    }

    public void OnStartGame()
    {
        if (PhotonNetwork.IsMasterClient && AllRolesSelected())
        {
            PhotonNetwork.LoadLevel("Bank Level");
        }
        else
        {
            feedbackText.text = "All roles must be filled to start.";
        }
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
        UpdateRoleButtons();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        UpdateRoleButtons();
    }
}
