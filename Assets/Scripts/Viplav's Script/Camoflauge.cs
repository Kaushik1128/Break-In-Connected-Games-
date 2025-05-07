using Unity.Cinemachine;
using UnityEngine;

public class Camouflage : MonoBehaviour
{
    public GameObject InsiderSkin;
    public GameObject GuardSkin;
    public GameObject gun;
    //public Transform GuardPos;
    private bool IsCamouflaged = false;
    public CinemachineCamera Cam1;
    public CinemachineCamera Cam2;

    void Start()
    {
        InsiderSkin.SetActive(true);
        GuardSkin.SetActive(false);
        gun.SetActive(false);
        Cam1.Priority = 20;
        Cam2.Priority = 10;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && !IsCamouflaged)
        {
            Cam2.Priority = 20;
            Cam1.Priority = 10;
            InsiderSkin.SetActive(false);
            GuardSkin.SetActive(true );
            //Instantiate(GuardSkin,GuardPos.transform.position, GuardPos.transform.rotation);
            gun.SetActive(true);
            IsCamouflaged = true;
            Debug.Log("Camouflaged into a guard");
        }
    }

}