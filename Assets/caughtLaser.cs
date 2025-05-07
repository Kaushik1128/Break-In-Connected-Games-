using UnityEngine;
using System.Collections;
using Photon.Pun;

public class CaughtLaser : MonoBehaviourPun
{
    private bool isTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!isTriggered && (other.CompareTag("LockPicker") || other.CompareTag("MainRobber")))
        {
            Debug.Log($"Laser triggered → Player caught: {other.tag}");
            isTriggered = true;

            StartCoroutine(HandleCaught());
        }
    }

    private IEnumerator HandleCaught()
    {
        yield return new WaitForSeconds(0.2f); // small delay for visual effect if needed

        CaughtUI caughtUI = FindObjectOfType<CaughtUI>();

        if (caughtUI != null && caughtUI.photonView != null)
        {
            caughtUI.photonView.RPC("ShowCaughtScreen", RpcTarget.All);
        }
        else
        {
            Debug.LogError("CaughtUI or PhotonView not found in scene!");
        }
    }
}
