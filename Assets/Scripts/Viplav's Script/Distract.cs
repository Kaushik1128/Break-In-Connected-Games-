using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.AI;

public class Distract : MonoBehaviour
{
    public NavMeshAgent agent;
    public Animator GuardAnimator;
    public Transform Waypoint1;
    public Transform Waypoint2;
    private bool IsDistracted = false;
    //public DistractedTimer = 10f;
    public Transform player;
    public float RotationSpeed = 5f;
    public float Timer = 0f;
    //private bool IsDistacted = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent> ();
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(Distraction());
        StartCoroutine(Caught());
    }
    IEnumerator Distraction()
    {
        if (Input.GetKeyDown(KeyCode.L) && !IsDistracted)
        {
            IsDistracted=true;
            GuardAnimator.SetBool("IsDistract", true);
            agent.SetDestination(Waypoint1.position);
            Timer += Time.deltaTime;
            Debug.Log("Guard Distracted");

            yield return new WaitForSeconds(10f);
            agent.SetDestination(Waypoint2.position);
            Debug.Log("Guard Returned");
            GuardAnimator.SetBool("IsDistract", true);
            yield return new WaitForSeconds(8f);
            Debug.Log("back to stationary position");
            GuardAnimator.SetBool("IsDistract", false);
            Debug.Log("time taken" + Timer);

        }
    }
    IEnumerator Caught()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < 4f)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            direction.y = 0;
            Quaternion TargetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, TargetRotation, Time.deltaTime * RotationSpeed);
            GuardAnimator.SetBool("IsCaught", true);
            yield return new WaitForSeconds(0.5f);
            agent.isStopped = true;
            
            yield return new WaitForSeconds(1.5f);
            Debug.Log("Caught");
            //GameOverUI.SetActive(true);

        }
    }
}

