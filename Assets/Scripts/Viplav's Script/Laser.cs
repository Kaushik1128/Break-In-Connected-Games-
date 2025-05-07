using UnityEngine;
using Photon.Pun;

public class Laser : MonoBehaviourPun
{
    public AudioSource laserSound;
    public GameObject CaughtUI;

    private void Start()
    {
        CaughtUI.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PhotonView pv = other.GetComponent<PhotonView>();
            if (pv != null && pv.IsMine)
            {
                // Only the local player sends the RPC
                photonView.RPC("CaughtPlayer", RpcTarget.All);
            }
        }
    }

    [PunRPC]
    void CaughtPlayer()
    {
        if (laserSound != null && !laserSound.isPlaying)
        {
            laserSound.Play();
        }

        if (CaughtUI != null)
        {
            CaughtUI.SetActive(true);
        }

        Debug.Log("Caught (RPC received)");
    }
}
