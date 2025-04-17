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
                PhotonNetwork.Instantiate("MainRobberPrefab", mainRobberSpawn.position, Quaternion.identity);
                break;
            case "LockPicker":
                PhotonNetwork.Instantiate("LockPickerPrefab", lockPickerSpawn.position, Quaternion.identity);
                break;
            case "Insider":
                PhotonNetwork.Instantiate("InsiderPrefab", insiderSpawn.position, Quaternion.identity);
                break;
            case "Hacker":
                PhotonNetwork.Instantiate("HackerPrefab", hackerSpawn.position, Quaternion.identity);
                break;
        }
    }
}
