using UnityEngine;
using TMPro;
using Photon.Pun;

public class ShowPasswordOnWall : MonoBehaviourPun
{
    public PasswordManager passwordManager;
    public TextMeshProUGUI passwordText;

    void Start()
    {
        // Only update on clients when password is already set
        if (passwordManager != null && passwordText != null)
        {
            // Check if the password has been initialized
            if (passwordManager.password != 0)
            {
                UpdateWallPassword(passwordManager.password);
            }
        }
        else
        {
            Debug.LogWarning("PasswordManager or TextMeshProUGUI is not assigned!");
        }
    }

    void Update()
    {
        // Keep checking in case the password arrives a bit later over the network
        if (passwordManager != null && passwordText != null && passwordManager.password != 0)
        {
            UpdateWallPassword(passwordManager.password);
            enabled = false; // Disable Update after setting once
        }

        
    }

    void UpdateWallPassword(int pw)
    {
        passwordText.text = pw.ToString();
        Debug.Log("Wall password updated: " + pw);
    }
}