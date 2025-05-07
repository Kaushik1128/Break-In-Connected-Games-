using UnityEngine;
using Photon.Pun;

public class PasswordTriggerHandler : MonoBehaviourPun
{
    public ManagerPassword managerPassword;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MainRobber"))
        {
            PhotonView view = other.GetComponent<PhotonView>();
            if (view != null && view.IsMine)
            {
                Debug.Log("[Trigger] MainRobber entered trigger");
                managerPassword.currentPlayer = other.gameObject;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MainRobber"))
        {
            PhotonView view = other.GetComponent<PhotonView>();
            if (view != null && view.IsMine)
            {
                Debug.Log("[Trigger] MainRobber exited trigger");
                managerPassword.currentPlayer = null;
            }
        }
    }
}
