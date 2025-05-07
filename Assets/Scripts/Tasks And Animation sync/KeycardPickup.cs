using UnityEngine;
using Photon.Pun;

public class KeycardPickup : MonoBehaviourPun
{
    //public GameObject instructionUI;
    public float pickupDistance = 2f;

    private GameObject insiderPlayer;

    private void Update()
    {
        if (insiderPlayer == null)
        {
            FindInsider();
            return;
        }

        if (!insiderPlayer.GetComponent<PhotonView>().IsMine) return;

        float distance = Vector3.Distance(transform.position, insiderPlayer.transform.position);

        if (distance <= pickupDistance)
        {
            //if (instructionUI != null)
                //instructionUI.SetActive(true);

            if (Input.GetKeyDown(KeyCode.T))
            {
                PlayerKeycardInventory.hasKeycard = true;
                photonView.RPC("DisableKeycard", RpcTarget.All);
            }
        }
        else
        {
            //if (instructionUI != null)
                //instructionUI.SetActive(false);
        }
    }

    void FindInsider()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Insider");
        foreach (var p in players)
        {
            if (p.GetComponent<PhotonView>().IsMine)
            {
                insiderPlayer = p;
                break;
            }
        }
    }

    [PunRPC]
    void DisableKeycard()
    {
        //if (instructionUI != null)
            //instructionUI.SetActive(false);

        gameObject.SetActive(false);
        Debug.Log("Keycard picked up!");
    }
}
