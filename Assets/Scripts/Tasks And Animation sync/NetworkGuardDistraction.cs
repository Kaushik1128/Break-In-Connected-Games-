using UnityEngine;
using System.Collections;
using Photon.Pun;
using UnityEngine.AI;

public class NetworkGuardDistraction : MonoBehaviourPun
{
    public NavMeshAgent agent;
    public Animator guardAnimator;
    public Transform atmPoint;
    public Transform patrolPoint;

    public float walkSpeed = 1.5f;
    public float normalSpeed = 3.5f;
    public float stopDistance = 1.0f;

    private bool isDistracted = false;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = 0f;  // default when patrolling
        agent.speed = normalSpeed;
    }

    public void TriggerDistraction()
    {
        if (!isDistracted)
        {
            photonView.RPC("StartDistraction", RpcTarget.All);
        }
    }

    [PunRPC]
    private void StartDistraction()
    {
        StartCoroutine(DistractionRoutine());
    }

    private IEnumerator DistractionRoutine()
    {
        isDistracted = true;

        // Walk slowly to ATM
        agent.speed = walkSpeed;
        agent.stoppingDistance = stopDistance;
        agent.SetDestination(atmPoint.position);
        guardAnimator.SetBool("IsDistract", true);
        Debug.Log("Guard walking to ATM");

        // Wait until near ATM
        while (Vector3.Distance(transform.position, atmPoint.position) > stopDistance + 0.1f)
        {
            yield return null;
        }

        // Stop moving at ATM
        agent.isStopped = true;
        Debug.Log("Guard reached ATM");

        yield return new WaitForSeconds(30f);
    

        // Return to patrol
        agent.isStopped = false;
        agent.speed = normalSpeed;
        agent.stoppingDistance = 0f;
        agent.SetDestination(patrolPoint.position);
        guardAnimator.SetBool("IsDistract", false);
        Debug.Log("Guard returning to patrol");
        Debug.Log("hi");
        isDistracted = false;
    }
}