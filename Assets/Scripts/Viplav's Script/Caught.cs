using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using Photon.Pun;

public class Caught : MonoBehaviourPun
{
    public Animator playerAnimator;
    public float rotationSpeed = 5f;
    public GameObject gameOverUI;
    public NavMeshAgent agent;
    //public Transform specialWaypoint;

    private GameObject currentTarget;
    private bool isInterrupted = false;

    void Start()
    {
        gameOverUI.SetActive(false);
    }

    void Update()
    {
        if (currentTarget != null && !isInterrupted)
        {
            float distance = Vector3.Distance(transform.position, currentTarget.transform.position);
            if (distance < 4f)
            {
                StartCoroutine(GameOverSequence());
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LockPicker") || other.CompareTag("MainRobber"))
            {
                currentTarget = other.gameObject;
                Debug.Log($"Caught detected: {other.tag}");
            }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == currentTarget)
        {
            currentTarget = null;
        }
    }

    IEnumerator GameOverSequence()
{
    isInterrupted = true;
    playerAnimator.SetBool("IsCaught", true);
    Vector3 direction = (currentTarget.transform.position - transform.position).normalized;
    direction.y = 0;
    Quaternion targetRotation = Quaternion.LookRotation(direction);
    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

    agent.isStopped = true;

    Debug.Log("Player caught!");

    yield return new WaitForSeconds(1.5f);

    // Find the CaughtUI object in the scene
    CaughtUI caughtUI = FindObjectOfType<CaughtUI>();

    if (caughtUI != null && caughtUI.photonView != null)
    {
        // Call the RPC across all players
        caughtUI.photonView.RPC("ShowCaughtScreen", RpcTarget.All);
    }
    else
    {
        Debug.LogError("CaughtUI or PhotonView not found in scene!");
    }
    gameOverUI.SetActive(true);
}
}
