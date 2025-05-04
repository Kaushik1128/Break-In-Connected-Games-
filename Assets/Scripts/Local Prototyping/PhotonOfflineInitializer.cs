using UnityEngine;
using Photon.Pun;

public class PhotonOfflineInitializer : MonoBehaviourPunCallbacks
{
    void Start()
    {
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.OfflineMode = true;
            PhotonNetwork.JoinRandomRoom();
        }
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Photon is running in offline mode for local testing.");
    }
}
