using System.Collections;
using UnityEngine;

public class GuardLocation : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject guard;
    Target targ;
    void Start()
    {
        targ = guard.GetComponent<Target>();
        targ.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(Check());
    }
    IEnumerator Check()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            targ.enabled = true;
            Debug.Log("Enemy Detection Mode On");
            targ.enabled = true;
            yield return new WaitForSeconds(10f);
            targ.enabled = false;
            Debug.Log("Enemy Detection Mode Off");
            targ.enabled=false;

        }
    }
}
