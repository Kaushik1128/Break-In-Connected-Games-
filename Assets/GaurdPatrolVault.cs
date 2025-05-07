using UnityEngine;
using Photon.Pun;

public class GuardSmoothMovement : MonoBehaviourPun
{
    public Transform distractionPoint;
    public Animator guardAnimator;
    public float moveSpeed = 2f;
    public float stopDistance = 0.1f;

    private bool isDistracted = false;

    void Update()
    {
        if (isDistracted)
        {
            // Smoothly move toward the distraction point
            transform.position = Vector3.MoveTowards(transform.position, distractionPoint.position, moveSpeed * Time.deltaTime);

            // Face toward the distraction point
            Vector3 direction = (distractionPoint.position - transform.position).normalized;
            if (direction != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
            }

            // Check if we reached the distraction point
            if (Vector3.Distance(transform.position, distractionPoint.position) <= stopDistance)
            {
                isDistracted = false;
                guardAnimator.SetBool("IsDistract", false); // Switch to idle or stop animation
            }
        }
    }

    [PunRPC]
    public void DistractGuardVault()
    {
        isDistracted = true;
        guardAnimator.SetBool("IsDistract", true); // Play walking animation
        Debug.Log("Guard is moving smoothly to distraction point.");
    }
}
