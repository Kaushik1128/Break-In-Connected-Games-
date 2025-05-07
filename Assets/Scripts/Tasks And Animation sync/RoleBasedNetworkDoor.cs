using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class NetworkDoorAllRoles : MonoBehaviourPun
{
    public GameObject instructionUI;
    public Animator doorAnimator;
    public AudioSource doorSound;
    public string openAnimation = "DoorOpen";
    public string closeAnimation = "DoorClose";

    private bool playerInRange = false;
    private bool isOpen = false;
    private GameObject currentPlayer;

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
            PhotonView playerPhotonView = other.GetComponent<PhotonView>();
            if (playerPhotonView != null && playerPhotonView.IsMine)
            {
                if (instructionUI != null)
                    instructionUI.SetActive(true);
            }
            playerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == currentPlayer)
        {
            PhotonView playerPhotonView = other.GetComponent<PhotonView>();
            if (playerPhotonView != null && playerPhotonView.IsMine)
            {
                if (instructionUI != null)
                    instructionUI.SetActive(false);
            }

            currentPlayer = null;
            playerInRange = false;
        }
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            photonView.RPC("ToggleDoor", RpcTarget.All);
        }
        else
        {
            Debug.LogWarning("⚠ PhotonView is missing on this object!");
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
}