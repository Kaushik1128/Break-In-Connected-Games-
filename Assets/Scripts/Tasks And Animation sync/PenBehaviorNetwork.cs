using UnityEngine;
using TMPro;
using Photon.Pun;

public class PenBehaviorNetwork : MonoBehaviourPun
{
    public TextMeshProUGUI passwordText;
    public GameObject passwordUI;
    public PasswordManager pass;
    private bool isPicked = false;
    private GameObject currentPlayer;

    void Start()
    {
        passwordUI.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MainRobber"))
        {
            currentPlayer = other.gameObject;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == currentPlayer)
        {
            currentPlayer = null;
        }
    }

    void Update()
    {
        if (currentPlayer != null && currentPlayer.GetComponent<PhotonView>().IsMine)
        {
            if (Vector3.Distance(transform.position, currentPlayer.transform.position) < 1.5f && Input.GetKeyDown(KeyCode.P) && !isPicked)
            {
                Debug.Log("Pen picked up");
                isPicked = true;
                int password = pass.password;
                photonView.RPC("ShowPasswordToAll", RpcTarget.All, password);
            }
        }
    }

    [PunRPC]
    void ShowPasswordToAll(int password)
    {
        passwordText.text = password.ToString();
        passwordUI.SetActive(true);
    }
}

