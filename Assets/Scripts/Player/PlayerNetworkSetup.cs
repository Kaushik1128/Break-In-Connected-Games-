using UnityEngine;
using Photon.Pun;

public class PlayerNetworkSetup : MonoBehaviour
{
    private PhotonView photonView;

    public GameObject cinemachineCamera; // drag your virtual camera here in Inspector
    public MonoBehaviour[] scriptsToEnable; // assign ThirdPersonController, StarterAssetsInputs, PlayerInput

    private void Awake()
    {
        photonView = GetComponent<PhotonView>();

        if (photonView.IsMine)
        {
            // Enable local player scripts
            foreach (MonoBehaviour script in scriptsToEnable)
            {
                script.enabled = true;
            }

            // Enable local camera
            cinemachineCamera.SetActive(true);
        }
    }
}