using UnityEngine;
using Photon.Pun;

public class DistractionButton : MonoBehaviour
{
    public GuardPatrol guard;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Insider") && Input.GetKeyDown(KeyCode.F))
        {
            guard.photonView.RPC("DistractGuard", RpcTarget.All);
        }
    }
}
