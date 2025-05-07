using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class NPCCharacterController : MonoBehaviour
{
    // Start is called before the first frame update
    // declares agent as NavMeshAgent 
    NavMeshAgent agent;
    // points that will guide the NPC as to where to walk
    public Transform waypoint;
    public Animator animator;
   
    void Start()
    {
        // get component of type NavMesh agent
        agent = GetComponent<NavMeshAgent>();
        SetAgentDestination();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Distract();
        // checks distance between player and the next waypoint position 
        float distance = Vector3.Distance(transform.position, waypoint.position);
        if (distance <= 2f)
        {
            // choosing a random number between 0 and the number of positions of waypoints in the list called "positions"
            int pos = Random.Range(0, waypoint.GetComponent<WayPointScript>().positions.Count - 1);
            // gives access to WayPOintScript and allow u to access the list which contains the positions
            // gives u a point from waypoint
            // gives u the position of the random waypoint number selected from the list of waypoints from another script whose reference we have taken here 
            waypoint = waypoint.GetComponent<WayPointScript>().positions[pos];
            // NPC will now go where the position is set for example waypoint[3] 
            agent.SetDestination(waypoint.position);
        }
    }
    void SetAgentDestination()
    {
        // to choose the maximum value we find the count of GameManager waypoints then subtract by 1 coz it starts from 0
        int pos = Random.Range(0, GameManager.instance.wayPoints.Count - 1);
        // to go to the random number chosen between 0 and 4 the position represents the transform part
        waypoint = GameManager.instance.wayPoints[pos];
        // like for example for above line it is waypoint[3] agent.SetDestination(waypoint.position) will set the NPC's position to waypoint[3]
        agent.SetDestination(waypoint.position);
    }
   

    }
