using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using StarterAssets;

public class CaughtUI : MonoBehaviourPun
{
    public GameObject Caught;
    public MonoBehaviour[] playerControllers;

    private bool isCaughtActive = false;

    void Start()
    {
        Caught.SetActive(false);
    }

    void Update()
    {
        if (isCaughtActive)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

                if (playerControllers != null)
                {
                    foreach (var controller in playerControllers)
                    {
                        if (controller != null)
                            controller.enabled = false;
                    }
                }
            }
    }

    [PunRPC]
    public void ShowCaughtScreen()
    {
        Caught.SetActive(true);
        isCaughtActive = true;
    }

    public void RestartGame()
    {
        photonView.RPC("ReloadSceneForAll", RpcTarget.All);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game Quit");
    }

    [PunRPC]
    void ReloadSceneForAll()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
