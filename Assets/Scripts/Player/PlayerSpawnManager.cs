using UnityEngine;
using Photon.Pun;

public class PlayerSpawnManager : MonoBehaviour
{
    public Transform mainRobberSpawn;
    public Transform lockPickerSpawn;
    public Transform insiderSpawn;
    public Transform hackerSpawn;

    private void Start()
    {
        SpawnPlayer();
    }

    void SpawnPlayer()
    {
        string role = PhotonNetwork.LocalPlayer.CustomProperties["SelectedRole"].ToString();

        switch (role)
        {
            case "MainRobber":
                PhotonNetwork.Instantiate("Main Robber", mainRobberSpawn.position, Quaternion.identity);
                break;
            case "LockPicker":
                PhotonNetwork.Instantiate("Lock Picker", lockPickerSpawn.position, Quaternion.identity);
                break;
            case "Insider":
                PhotonNetwork.Instantiate("Insider", insiderSpawn.position, Quaternion.identity);
                break;
            case "Hacker":
                PhotonNetwork.Instantiate("Hacker", hackerSpawn.position, Quaternion.identity);
                break;
        }

        Debug.Log("Spawning role: " + role);
    }
}
