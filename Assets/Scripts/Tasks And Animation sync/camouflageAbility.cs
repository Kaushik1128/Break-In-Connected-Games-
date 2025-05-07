using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;

public class CamouflageAbility : MonoBehaviourPun
{
    public GameObject tShirt;
    public GameObject armour;
    public GameObject helmet;
    public GameObject shirt;

    public TMPro.TMP_Text timerText; // Assign a UI Text in the inspector

    public float abilityDuration = 30f;
    private float timer = 0f;
    private bool isInTrigger = false;
    private bool isActive = false;

    private void Update()
    {
        if (!photonView.IsMine) return;

        // Check if this player has the "Insider" tag
        if (isInTrigger && Input.GetKeyDown(KeyCode.C) && CompareTag("Insider") && !isActive)
        {
            photonView.RPC("ActivateCamouflage", RpcTarget.All);
        }

        if (isActive)
        {
            timer -= Time.deltaTime;
            timerText.text = "Camouflage: " + Mathf.Ceil(timer).ToString();

            if (timer <= 0)
            {
                photonView.RPC("DeactivateCamouflage", RpcTarget.All);
            }
        }
    }

    [PunRPC]
    void ActivateCamouflage()
    {
        tShirt.SetActive(false);
        armour.SetActive(true);
        helmet.SetActive(true);
        shirt.SetActive(true);

        timer = abilityDuration;
        isActive = true;
    }

    [PunRPC]
    void DeactivateCamouflage()
    {
        tShirt.SetActive(true);
        armour.SetActive(false);
        helmet.SetActive(false);
        shirt.SetActive(false);

        timerText.text = "";
        isActive = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CamouflageZone") && photonView.IsMine)
        {
            isInTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("CamouflageZone") && photonView.IsMine)
        {
            isInTrigger = false;
        }
    }

    public bool GetIsActive()
    {
        return isActive;
    }
}