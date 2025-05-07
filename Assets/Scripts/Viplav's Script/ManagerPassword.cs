using UnityEngine;
using TMPro;
using Photon.Pun;
using UnityEngine.UI;
using System.Collections;

public class ManagerPassword : MonoBehaviourPun
{
    public GameObject pcCanvas;
    public GameObject VaultUI;
    public TMP_InputField passwordField;
    public TextMeshProUGUI feedbackText;
    public TextMeshProUGUI vaultOpenedText;  // <- NEW: Text element for vault status
    public PasswordManager pass;

    public MonoBehaviour ThirdPersonController;
    public GameObject Bankmap;
    public Button myButton;
    public Button vaultOpenButton;  // <- NEW: Vault Open button
    public int count = 0;

    [HideInInspector] public GameObject currentPlayer;

    void Start()
    {
        Debug.Log("[ManagerPassword] Script active");

        myButton.onClick.AddListener(() => StartCoroutine(BankMap()));
        vaultOpenButton.onClick.AddListener(OpenVault);  // <- Hook vault button

        Bankmap.SetActive(false);
        pcCanvas.SetActive(false);
        VaultUI.SetActive(false);

        if (vaultOpenedText != null)
            vaultOpenedText.gameObject.SetActive(false);  // <- Hide vault text at start
    }

    void Update()
    {
        if (currentPlayer != null && Input.GetKeyDown(KeyCode.U))
        {
            PhotonView view = currentPlayer.GetComponent<PhotonView>();
            if (view != null && view.IsMine)
            {
                Debug.Log("[ManagerPassword] U key pressed by local MainRobber → opening PC UI");
                OpenPCUI();
            }
        }
    }

    public void OpenPCUI()
    {
        pcCanvas.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (ThirdPersonController != null)
            ThirdPersonController.enabled = false;
    }

    public void SubmitPassword()
    {
        string correctPassword = pass.password.ToString();
        if (passwordField.text == correctPassword)
        {
            feedbackText.text = "Access Granted! Go to the vault and press [E].";
            Invoke(nameof(ClosePCUI), 1f);
        }
        else
        {
            feedbackText.text = "Wrong Password!";
        }
    }

    public void ClosePCUI()
    {
        pcCanvas.SetActive(false);
        VaultUI.SetActive(true);
        Debug.Log("[ManagerPassword] Access Granted → Vault UI shown");
    }

    public void StartMyCoroutine()
    {
        StartCoroutine(BankMap());
    }

    IEnumerator BankMap()
    {
        Bankmap.SetActive(true);
        count++;
        Debug.Log("[ManagerPassword] Showing bank map, count: " + count);
        yield return new WaitForSeconds(5f);
        Bankmap.SetActive(false);
    }

    public void LogOut()
    {
        VaultUI.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        if (ThirdPersonController != null)
            ThirdPersonController.enabled = true;
    }

    public void OpenVault()
    {
        Debug.Log("[ManagerPassword] Vault open button clicked");
        if (vaultOpenedText != null)
        {
            vaultOpenedText.text = "Vault is Opened!";
            vaultOpenedText.gameObject.SetActive(true);
            Invoke(nameof(HideVaultText), 3f);
        }
    }

    private void HideVaultText()
    {
        if (vaultOpenedText != null)
            vaultOpenedText.gameObject.SetActive(false);
    }
}
