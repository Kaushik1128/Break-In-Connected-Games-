using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PenBehavior : MonoBehaviour
{
    public TextMeshProUGUI passwordText;
    public GameObject passwordUI;
    public PasswordManager pass;
    public GameObject player;
        private bool IsPicked = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //pass = GetComponent<PasswordManager>();
        passwordUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < 1.5f && Input.GetKey(KeyCode.P) && !IsPicked)
        {
            Debug.Log("Pen picked up");
            IsPicked = true;
            int Password = pass.password;
            passwordText.text = Password.ToString();
            passwordUI.SetActive(true);
        }
    }
}
