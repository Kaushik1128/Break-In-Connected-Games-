using UnityEngine;
using Photon.Pun;

public class PasswordManager : MonoBehaviourPun
{
    public int password;

    void Awake()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            int randomPassword = Random.Range(1000, 9999);
            photonView.RPC("SetPassword", RpcTarget.AllBuffered, randomPassword);
        }
    }

    [PunRPC]
    void SetPassword(int pw)
    {
        password = pw;
        Debug.Log("Password set to: " + pw);
    }
}
