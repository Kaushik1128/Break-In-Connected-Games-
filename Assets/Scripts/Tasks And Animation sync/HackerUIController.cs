using UnityEngine;
using System.Collections;
using Photon.Pun;
using TMPro;
using UnityEngine.UI;

public class HackerUIController : MonoBehaviourPun
{
    [Header("UI Panels")]
    public GameObject hackerMainUI;
    public GameObject upperFloorMapUI;
    public GameObject basementMapUI;
    public TextMeshProUGUI cooldownText;

    [Header("Cooldown Settings")]
    public float revealDuration = 15f;
    public float cooldownDuration = 20f;

    private bool isOnCooldown = false;
    private bool showingUpperMap = true;

    private void Start()
    {
        if (!photonView.IsMine)
        {
            hackerMainUI.SetActive(false);
            enabled = false;
            return;
        }

        // Disable movement
        //MonoBehaviour movementScript = GetComponent<ThirdPersonController>();
        //if (movementScript != null)
            //movementScript.enabled = false;

        hackerMainUI.SetActive(true);
        upperFloorMapUI.SetActive(false);
        basementMapUI.SetActive(false);

        cooldownText.text = "";
    }

    private void Update()
    {
        if (!photonView.IsMine) return;

        if (Input.GetKeyDown(KeyCode.M))
        {
            ToggleMinimap();
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            ActivateReveal();
        }
    }

    private void ActivateReveal()
    {
        if (isOnCooldown) return;

        photonView.RPC("RPC_RevealTargets", RpcTarget.All);
        StartCoroutine(CooldownRoutine());
    }

    [PunRPC]
    private void RPC_RevealTargets()
    {
        StartCoroutine(RevealTargetsCoroutine());
    }

    private IEnumerator RevealTargetsCoroutine()
    {
        Target[] allTargets = FindObjectsOfType<Target>();
        foreach (var target in allTargets)
        {
            target.enabled = true;
        }

        yield return new WaitForSeconds(revealDuration);

        foreach (var target in allTargets)
        {
            target.enabled = false;
        }
    }

    private IEnumerator CooldownRoutine()
    {
        isOnCooldown = true;
        float elapsed = 0f;

        while (elapsed < cooldownDuration)
        {
            elapsed += Time.deltaTime;
            float remaining = cooldownDuration - elapsed;
            cooldownText.text = $"Cooldown: {remaining:F1}s";
            yield return null;
        }

        cooldownText.text = "";
        isOnCooldown = false;
    }

    private void ToggleMinimap()
    {
        showingUpperMap = !showingUpperMap;
        upperFloorMapUI.SetActive(showingUpperMap);
        basementMapUI.SetActive(!showingUpperMap);
    }
}
