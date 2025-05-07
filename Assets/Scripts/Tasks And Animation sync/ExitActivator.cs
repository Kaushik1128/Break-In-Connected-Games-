using UnityEngine;
using Photon.Pun;

public class ExitActivator : MonoBehaviourPun
{
    private Target targetComponent;

    void Start()
    {
        targetComponent = GetComponent<Target>();
        targetComponent.enabled = false;
    }

    void Update()
    {
        if (HeistGameState.hasGold && HeistGameState.hackerSuccess)
        {
            if (!targetComponent.enabled)
            {
                photonView.RPC("EnableExitTarget", RpcTarget.All);
            }
        }
    }

    [PunRPC]
    void EnableExitTarget()
    {
        targetComponent.enabled = true;
        Debug.Log("Escape Route Activated!");
    }
}
