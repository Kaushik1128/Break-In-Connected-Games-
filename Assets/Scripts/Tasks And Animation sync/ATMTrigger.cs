using UnityEngine;

public class ATMTrigger : MonoBehaviour
{
    private bool playerInRange = false;
    private NetworkGuardDistraction guardScript;

    [Header("Audio")]
    public AudioSource distractionAudio;
    public float distractionDuration = 30f; 

    private void Start()
    {
        guardScript = FindObjectOfType<NetworkGuardDistraction>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Insider"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Insider"))
        {
            playerInRange = false;
        }
    }

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.F))
        {
            guardScript.TriggerDistraction();
            PlayDistractionAudio();
        }
    }

    private void PlayDistractionAudio()
    {
        if (distractionAudio != null)
        {
            distractionAudio.Play();
            Invoke(nameof(StopDistractionAudio), distractionDuration);
        }
    }

    private void StopDistractionAudio()
    {
        if (distractionAudio.isPlaying)
        {
            distractionAudio.Stop();
        }
    }
}