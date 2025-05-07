using Photon.Pun;
using UnityEngine;

public class DistractionButtonVault : MonoBehaviourPun
{
    public GuardSmoothMovement guard;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Insider") && Input.GetKeyDown(KeyCode.F))
        {
            guard.photonView.RPC("DistractGuardVault", RpcTarget.All);
        }
    }
}
