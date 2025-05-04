using UnityEngine;
using Unity.Cinemachine;
using Photon.Pun;

public class PlayerCameraSetup : MonoBehaviour
{
    private PhotonView photonView;
    void Start()
    {
        photonView = GetComponent<PhotonView>();

        if (!photonView.IsMine)
        {
            CinemachineCamera virtualCam = GetComponentInChildren<CinemachineCamera>();
            if (virtualCam != null)
            {
                virtualCam.enabled = false;
            }
        }
    }
}

