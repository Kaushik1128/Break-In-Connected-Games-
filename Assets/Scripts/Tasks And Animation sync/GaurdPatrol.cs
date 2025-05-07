using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;
using System.Collections;

public class GuardPatrol : MonoBehaviourPun
{
    public NavMeshAgent agent;
    public Animator guardAnimator;
    public Transform patrolPoint;        // where the guard stays idle
    public Transform distractionPoint;  // where the guard moves when distracted

    public float walkSpeed = 2f;
    public float stopDistance = 1f;

    private bool isDistracted = false;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = 0f;  // stationary at start
        agent.stoppingDistance = 0f;
        agent.SetDestination(patrolPoint.position);
    }

    private void Update()
    {
        if (!isDistracted)
        {
            agent.speed = 0f;  // stay still
        }
    }

    [PunRPC]
    public void DistractGuard()
    {
        if (!isDistracted)
            StartCoroutine(DistractRoutine());
    }

    private IEnumerator DistractRoutine()
    {
        isDistracted = true;

        // Step 1 → Walk to distraction point
        agent.speed = walkSpeed;
        agent.stoppingDistance = stopDistance;
        agent.SetDestination(distractionPoint.position);
        guardAnimator.SetBool("IsDistract", true);
        Debug.Log("Guard is walking to distraction point");

        // Wait until near the distraction point
        while (Vector3.Distance(transform.position, distractionPoint.position) > stopDistance + 0.1f)
        {
            yield return null;
        }

        // Step 2 → Stop at distraction point
        agent.isStopped = true;
        Debug.Log("Guard reached distraction point");

        yield return new WaitForSeconds(20f);

        // Step 3 → Return to patrol point
        agent.isStopped = false;
        agent.SetDestination(patrolPoint.position);
        Debug.Log("Guard returning to patrol point");

        // Wait until near patrol point
        while (Vector3.Distance(transform.position, patrolPoint.position) > stopDistance + 0.1f)
        {
            yield return null;
        }

        // Step 4 → Stop and reset
        agent.speed = 0f;
        agent.isStopped = true;
        guardAnimator.SetBool("IsDistract", false);
        Debug.Log("Guard back at patrol point");

        isDistracted = false;
    }
}
