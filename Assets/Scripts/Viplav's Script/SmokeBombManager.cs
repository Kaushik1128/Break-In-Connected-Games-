using UnityEngine;
using System.Collections;

public class SmokeTrigger : MonoBehaviour
{
    public GameObject[] lasers;                      // Assign all laser GameObjects here
    public ParticleSystem[] smokeParticles;         // Assign 4 smoke particle systems here
    public GameObject instructionUI;               // Assign the “Press G for smoke bomb” UI
    private bool playerInRange = false;
    private bool hasTriggered = false;

    void Start()
    {
        instructionUI.SetActive(false);
        foreach (ParticleSystem ps in smokeParticles)
        {
            ps.Stop();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LockPicker") && !hasTriggered)
        {
            playerInRange = true;
            instructionUI.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("LockPicker"))
        {
            playerInRange = false;
            instructionUI.SetActive(false);
        }
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.G) && !hasTriggered)
        {
            StartCoroutine(ActivateSmokeBomb());
        }
    }

    IEnumerator ActivateSmokeBomb()
    {
        hasTriggered = true;
        instructionUI.SetActive(false);

        // Disable all lasers permanently
        foreach (GameObject laser in lasers)
        {
            laser.SetActive(false);
        }

        // Play all smoke particles
        foreach (ParticleSystem ps in smokeParticles)
        {
            ps.Play();
        }

        // Smoke runs for 5 seconds, but lasers stay disabled
        yield return new WaitForSeconds(5f);

        // Stop smoke particles after 5 sec
        foreach (ParticleSystem ps in smokeParticles)
        {
            ps.Stop();
        }
    }
}
