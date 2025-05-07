using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class LockPicker : MonoBehaviour
{
    public GameObject LockPickerUI; // Assign your PC UI Canvas here
    //public GameObject VaultUI;
    //public TMP_InputField passwordField;
    //public TextMeshProUGUI feedbackText;
    //public PasswordManager pass;

    public GameObject player; // To disable movement
    public MonoBehaviour ThirdPersonController;

    void Start()
    {
        LockPickerUI.SetActive(false);
        //VaultUI.SetActive(false);
    }// Your movement script

    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < 1.5f && Input.GetKeyDown(KeyCode.M)) // Example interaction key
        {
            // you can use a trigger check or distance

            OpenLockUI();

        }
    }

    public void OpenLockUI()
    {
        LockPickerUI.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (ThirdPersonController != null)
            ThirdPersonController.enabled = false;
    }
    public void LogOut()
    {
        LockPickerUI.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        if (ThirdPersonController != null)
            ThirdPersonController.enabled = true;




    }
}

