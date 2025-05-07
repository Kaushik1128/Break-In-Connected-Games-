using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class CartMovement : MonoBehaviour
{
    public GameObject player;
    public GameObject CartPos;
    public GameObject cart;
    private bool IsPushed = false;
    public Animator playerAnim;
    void Start()
    {

    }
    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < 1.5f && Input.GetKey(KeyCode.U) && !IsPushed)
        {
            playerAnim.SetBool("IsPushing", true);
            IsPushed = true;

            transform.position = CartPos.transform.position;
            //transform.rotation = CartPos.transform.rotation;
            cart.transform.SetParent(CartPos.transform, true);
            Debug.Log("Pushing cart");
        }
        if (Vector3.Distance(transform.position, player.transform.position) < 1.5f && Input.GetKeyUp(KeyCode.U) && IsPushed)
        {
            playerAnim.SetBool("IsPushing", false);
            IsPushed = false;
            transform.position = CartPos.transform.position;
            //transform.rotation = CartPos.transform.rotation;
            cart.transform.SetParent(null);
            Debug.Log("Done with Pushing cart");

        }

    }
}

    /*private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(leaderTag))
        {
            isBeingPushed = false;
        }
    }
}
    */