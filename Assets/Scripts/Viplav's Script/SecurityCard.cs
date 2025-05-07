using UnityEngine;

public class SecurityCard : MonoBehaviour
{
    private bool IsStolen = false;
    public GameObject SecurityCardPos;
    public GameObject Securitycard;
    //public GameObject SecurityCardUI;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //SecurityCardUI.SetActive(false);
        Securitycard.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, Securitycard.transform.position) < 1.5f && Input.GetKeyDown(KeyCode.J) && !IsStolen)
        {
            Securitycard.SetActive(false);
            IsStolen = true;
            Debug.Log("Security Card Stolen");

        }
        if (Vector3.Distance(transform.position, SecurityCardPos.transform.position) < 1.5f && Input.GetKeyDown(KeyCode.J) && IsStolen)
        {
            Securitycard.SetActive(true);
            IsStolen = false;
            Debug.Log("Security Card accessed to open door");
            Securitycard.transform.SetParent(SecurityCardPos.transform, true);
            Securitycard.transform.position = SecurityCardPos.transform.position;
            Securitycard.transform.rotation = SecurityCardPos.transform.rotation;
        }

    }
}
