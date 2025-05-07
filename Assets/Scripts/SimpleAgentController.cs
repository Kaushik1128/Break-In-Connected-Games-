using UnityEngine;
using UnityEngine.AI;

public class SimpleAgentController : MonoBehaviour
{
    public Transform target;  // Assign in Inspector
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (target != null)
        {
            agent.SetDestination(target.position);
        }
    }
}