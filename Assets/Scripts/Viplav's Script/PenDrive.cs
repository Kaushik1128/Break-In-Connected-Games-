using UnityEngine;
using Photon.Pun;

public class PenDriveManager : MonoBehaviourPun
{
    private GameObject insiderPlayer;
    public GameObject penDriveObject;

    public void SetInsiderPlayer(GameObject player)
    {
        insiderPlayer = player;
    }

    public void ClearInsiderPlayer()
    {
        insiderPlayer = null;
    }

    void Update()
    {
        if (insiderPlayer != null && insiderPlayer.GetComponent<PhotonView>().IsMine && Input.GetKeyDown(KeyCode.I))
        {
            photonView.RPC("InsertPendrive", RpcTarget.All);
        }
    }

    [PunRPC]
    void InsertPendrive()
    {
        penDriveObject.SetActive(true);
        ActivateHackerUI();
    }

    void ActivateHackerUI()
    {
        foreach (TypingMiniGame miniGame in FindObjectsOfType<TypingMiniGame>())
        {
            PhotonView hackerPV = miniGame.GetComponent<PhotonView>();
            if (hackerPV != null)
            {
                // Only send the RPC to the hacker’s OWNER, not to all players
                hackerPV.RPC("EnableMiniGame", hackerPV.Owner);
            }
        }
    }
}
