using System.Collections;
using UnityEngine;

public class HackerVision : MonoBehaviour
{
    public Material transparentWallMaterial;
    public Material normalWallMaterial;

    //public Material redEnemyMaterial;
    //public Material normalEnemyMaterial;

    public float visionDuration = 5f;     // How long the vision lasts
    public float cooldownDuration = 10f;  // Cooldown after activation

    private GameObject[] walls;
    //private GameObject[] enemies;

    private bool isOnCooldown = false;

    void Start()
    {
        walls = GameObject.FindGameObjectsWithTag("Wall");
        //enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H) && !isOnCooldown)
        {
            StartCoroutine(HackerVisionRoutine());
        }
    }

    private IEnumerator HackerVisionRoutine()
    {
        isOnCooldown = true;

        // Make walls transparent
        foreach (GameObject wall in walls)
        {
            Renderer rend = wall.GetComponent<Renderer>();
            if (rend) rend.material = transparentWallMaterial;
        }

        // Make enemies red
        /*foreach (GameObject enemy in enemies)
        {
            Renderer rend = enemy.GetComponent<Renderer>();
            if (rend) rend.material = redEnemyMaterial;
        }
        */
        yield return new WaitForSeconds(visionDuration);

        // Revert walls
        foreach (GameObject wall in walls)
        {
            Renderer rend = wall.GetComponent<Renderer>();
            if (rend) rend.material = normalWallMaterial;
        }

        // Revert enemies
        /*foreach (GameObject enemy in enemies)
        {
            Renderer rend = enemy.GetComponent<Renderer>();
            if (rend) rend.enabled = false;
        }
        */
        // Wait for cooldown to finish
        yield return new WaitForSeconds(cooldownDuration);
        isOnCooldown = false;
    }
}