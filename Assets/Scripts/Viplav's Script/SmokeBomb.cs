
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeBomb : MonoBehaviour
{
    // Start is called before the first frame update
    public ParticleSystem particle1;
    public ParticleSystem particle2;
    public ParticleSystem particle3;
    public ParticleSystem particle4;
    private bool HasCollided = false;
    //public GameObject Laser[];
    void Start()
    {
        ParticleSystem particle1 = GetComponent<ParticleSystem>();
        ParticleSystem particle2 = GetComponent<ParticleSystem>();
        ParticleSystem particle3 = GetComponent<ParticleSystem>();
        ParticleSystem particle4 = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (HasCollided)
        {
            StartCoroutine(LaserDisable());
            HasCollided = false;
        }
    }


    public void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Laser"))
        {
            HasCollided = true;
        }
    }
    IEnumerator LaserDisable()
    {

        particle1.Play();
        particle2.Play();
        particle3.Play();
        particle4.Play();
        GameObject[] Lasers = GameObject.FindGameObjectsWithTag("Laser");
        foreach (GameObject laser in Lasers)
        {
            laser.SetActive(false);

        }

        yield return new WaitForSeconds(5f);
        Debug.Log("Laser ReActivated");
        foreach (GameObject laser in Lasers)
        {
            if (laser != null)
            {

                Debug.Log("Reactivating Laser");

                laser.SetActive(true);
            }

        }
    }
}


    

