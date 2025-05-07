using UnityEngine;
using Photon.Pun;

public class GoldPickup : MonoBehaviourPun
{
    public float pickupDistance = 2f;

    private GameObject mainRobberPlayer;

    private void Update()
    {
        if (mainRobberPlayer == null)
        {
            FindMainRobber();
            return;
        }

        if (!mainRobberPlayer.GetComponent<PhotonView>().IsMine) return;

        float distance = Vector3.Distance(transform.position, mainRobberPlayer.transform.position);

        if (distance <= pickupDistance)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                //HeistGameState.hasGold += 1;
                photonView.RPC("DisableGoldObject", RpcTarget.All);
            }
        }
    }

    void FindMainRobber()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("MainRobber");
        foreach (var p in players)
        {
            if (p.GetComponent<PhotonView>().IsMine)
            {
                mainRobberPlayer = p;
                break;
            }
        }
    }

    [PunRPC]
    
    void DisableGoldObject()
    {
        gameObject.SetActive(false);
        HeistGameState.hasGold = true;
        Debug.Log("Gold picked up! Total gold: " + HeistGameState.hasGold);
    }

    
}
