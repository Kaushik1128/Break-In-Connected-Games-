using UnityEngine;
using Photon.Pun;

public class ServerTrigger : MonoBehaviourPun
{
    public PenDriveManager penDrive;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Insider"))
        {
            PhotonView pv = other.GetComponent<PhotonView>();
            if (pv != null && pv.IsMine)
            {
                Debug.Log("Insider Entered (local)");
                penDrive.SetInsiderPlayer(other.gameObject);
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
                Debug.Log("Insider Exited (local)");
                penDrive.ClearInsiderPlayer();
            }
        }
    }
}

