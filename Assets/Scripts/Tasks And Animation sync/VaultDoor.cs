using UnityEngine;
using Photon.Pun;
using TMPro;

public class VaultDoor : MonoBehaviourPun
{
    public Animator vaultAnimator;
    public TextMeshProUGUI feedbackText;  // reference to the feedbackText from ManagerPassword
    private bool isUnlocked = false;
    private bool playerNearVault = false;
    private GameObject currentPlayer;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MainRobber") || other.CompareTag("LockPicker") || other.CompareTag("Hacker") || other.CompareTag("Insider"))
        {
            currentPlayer = other.gameObject;
            playerNearVault = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == currentPlayer)
        {
            currentPlayer = null;
            playerNearVault = false;
        }
    }

    void Update()
    {
        if (playerNearVault && isUnlocked && Input.GetKeyDown(KeyCode.E))
        {
            photonView.RPC("NetworkOpenVault", RpcTarget.All);
        }
    }

    public void UnlockVault()
    {
        isUnlocked = true;
    }

    [PunRPC]
    void NetworkOpenVault()
    {
        if (!vaultAnimator.GetBool("IsOpen"))  // make sure we don’t reopen
        {
            vaultAnimator.SetTrigger("Open");
            vaultAnimator.SetBool("IsOpen", true);
            if (feedbackText != null)
            {
                feedbackText.text = "Vault successfully opened!";
            }
        }
    }
}
