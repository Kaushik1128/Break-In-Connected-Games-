using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using TMPro;

public class EscapeTrigger : MonoBehaviourPun
{
    public GameObject gameOverUI;
    public TextMeshProUGUI instructionText;
    public string loginSceneName = "Login Scene";

    private void Start()
    {
        if (gameOverUI != null)
            gameOverUI.SetActive(false);

        if (instructionText != null)
            instructionText.text = "";
    }

    private void OnTriggerStay(Collider other)
    {
        if (!HeistGameState.hasGold || !HeistGameState.hackerSuccess)
        {
            if (instructionText != null)
                instructionText.text = "Complete all tasks to escape!";
            return;
        }

        if (other.CompareTag("MainRobber") && Input.GetKeyDown(KeyCode.E))
        {
            HeistGameState.mainRobberReady = true;
        }

        if (other.CompareTag("LockPicker") && Input.GetKeyDown(KeyCode.E))
        {
            HeistGameState.lockPickerReady = true;
        }

        if (other.CompareTag("Insider") && Input.GetKeyDown(KeyCode.E))
        {
            HeistGameState.insiderReady = true;
        }

        UpdateInstructions();

        if (HeistGameState.mainRobberReady && HeistGameState.lockPickerReady && HeistGameState.insiderReady)
        {
            photonView.RPC("ShowGameOver", RpcTarget.All);
        }
    }

    private void UpdateInstructions()
    {
        string msg = "Waiting for: ";
        if (!HeistGameState.mainRobberReady) msg += "Main Robber ";
        if (!HeistGameState.lockPickerReady) msg += "Lock Picker ";
        if (!HeistGameState.insiderReady) msg += "Insider ";

        instructionText.text = msg.Trim();
    }

    [PunRPC]
    void ShowGameOver()
    {
        if (gameOverUI != null)
            gameOverUI.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Invoke(nameof(ReturnToLogin), 5f);
    }

    void ReturnToLogin()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel(loginSceneName);
            HeistGameState.Reset();
        }
    }
}
