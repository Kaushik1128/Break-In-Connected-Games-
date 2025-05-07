using UnityEngine;
using System.Collections;
using Photon.Pun;

public class NetworkLockPickerDoor : MonoBehaviourPun
{
    public GameObject instructionUI;
    public TMPro.TMP_Text instructionText;
    public Animator doorAnimator;
    public AudioSource doorSound;
    public string openAnimation = "DoorOpen";
    public string closeAnimation = "DoorClose";

    private bool playerInRange = false;
    private bool isOpen = false;
    private GameObject currentPlayer;

    private float holdTime = 5f;
    private float holdTimer = 0f;

    void Start()
    {
        if (instructionUI != null)
            instructionUI.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (IsPlayerTag(other.tag))
        {
            currentPlayer = other.gameObject;
            PhotonView playerPV = currentPlayer.GetComponent<PhotonView>();

            if (playerPV != null && playerPV.IsMine && instructionUI != null)
            {
                instructionUI.SetActive(true);
                UpdateInstructionText(other.tag);
            }

            playerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == currentPlayer)
        {
            PhotonView playerPV = currentPlayer.GetComponent<PhotonView>();

            if (playerPV != null && playerPV.IsMine && instructionUI != null)
            {
                instructionUI.SetActive(false);
            }

            currentPlayer = null;
            playerInRange = false;
            holdTimer = 0f;
        }
    }


    void Update()
    {
        if (!playerInRange || currentPlayer == null) return;

        if (currentPlayer.CompareTag("LockPicker"))
        {
            if (Input.GetKey(KeyCode.E))
            {
                holdTimer += Time.deltaTime;
                instructionText.text = $"Holding... {holdTimer:F1}/{holdTime} sec";

                if (holdTimer >= holdTime)
                {
                    photonView.RPC("ToggleDoor", RpcTarget.All);
                    holdTimer = 0f;
                }
            }
            else if (Input.GetKeyUp(KeyCode.E))
            {
                holdTimer = 0f;
                instructionText.text = "Hold [E] to unlock";
            }
        }
    }

    [PunRPC]
    void ToggleDoor()
    {
        if (instructionUI != null)
            instructionUI.SetActive(false);

        if (doorAnimator != null)
        {
            string animationToPlay = isOpen ? closeAnimation : openAnimation;
            doorAnimator.Play(animationToPlay);
        }

        if (doorSound != null)
            doorSound.Play();

        isOpen = !isOpen;
    }

    private bool IsPlayerTag(string tag)
    {
        return tag == "MainRobber" || tag == "Hacker" || tag == "LockPicker" || tag == "Insider";
    }

    private void UpdateInstructionText(string tag)
    {
        if (instructionText == null) return;

        if (tag == "LockPicker")
            instructionText.text = "Hold [E] to unlock";
        else
            instructionText.text = "Locked";
    }
}