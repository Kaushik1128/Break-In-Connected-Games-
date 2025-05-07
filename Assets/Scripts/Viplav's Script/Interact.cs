using UnityEngine;

public class Interact : MonoBehaviour

{
    public GameObject InteractUI;
    public GameObject player;
    //private bool HasInteract = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InteractUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (Vector3.Distance(transform.position, player.transform.position) < 1.5f)
        {
            InteractUI.SetActive(true);
        }
        if (Vector3.Distance(transform.position, player.transform.position) > 1.5f)
        {
            InteractUI.SetActive(false);
        }
    }
}
