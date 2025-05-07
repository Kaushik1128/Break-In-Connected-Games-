using UnityEngine;
using Photon.Pun;

public class InteractNetwork : MonoBehaviourPun
{
    public GameObject InteractUI;

    private void Start()
    {
        if (InteractUI != null)
            InteractUI.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MainRobber"))
        {
            PhotonView playerPhotonView = other.GetComponent<PhotonView>();
            if (playerPhotonView != null && playerPhotonView.IsMine)
            {
                InteractUI.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MainRobber"))
        {
            PhotonView playerPhotonView = other.GetComponent<PhotonView>();
            if (playerPhotonView != null && playerPhotonView.IsMine)
            {
                InteractUI.SetActive(false);
            }
        }
    }
}
