using UnityEngine;
using Photon.Pun;

public class KeycardDoor : MonoBehaviourPun
{
    public Animator doorAnimator;
    public AudioSource doorSound;
    public string openAnimation = "BasementGateOpen";
    public string closeAnimation = "BasementGateClose";

    private bool isOpen = false;
    private bool insiderInTrigger = false;
    private GameObject insiderPlayer;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Insider"))
        {
            PhotonView pv = other.GetComponent<PhotonView>();
            if (pv != null && pv.IsMine)
            {
                insiderInTrigger = true;
                insiderPlayer = other.gameObject;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Insider"))
        {
            PhotonView pv = other.GetComponent<PhotonView>();
            if (pv != null && pv.IsMine)
            {
                insiderInTrigger = false;
                insiderPlayer = null;
            }
        }
    }

    private void Update()
    {
        if (insiderInTrigger && insiderPlayer != null)
        {
            if (Input.GetKeyDown(KeyCode.O))
            {
                if (PlayerKeycardInventory.hasKeycard)
                {
                    photonView.RPC("ToggleDoor", RpcTarget.All);
                }
                else
                {
                    Debug.Log("Insider does not have the keycard!");
                }
            }
        }
    }

    [PunRPC]
    void ToggleDoor()
{
    if (doorAnimator != null)
    {
        if (isOpen)
            doorAnimator.SetTrigger("Close");
        else
            doorAnimator.SetTrigger("Open");
    }

    if (doorSound != null)
        doorSound.Play();

    isOpen = !isOpen;
}
}
