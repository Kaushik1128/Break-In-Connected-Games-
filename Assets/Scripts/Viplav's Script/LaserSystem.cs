using UnityEngine;

public class LaserSystem : MonoBehaviour
{
    public bool usedSmoke = false;
    private bool isActive = true;
    public GameObject laserVisual; // optional, disable beam visuals

    public void DisableTemporarily(float duration)
    {
        if (usedSmoke) return;

        usedSmoke = true;
        isActive = false;
        if (laserVisual) laserVisual.SetActive(false); // optional visual off
        GetComponent<Collider>().enabled = false; // disable detection

        Invoke(nameof(ReenableLaser), duration);
    }

    void ReenableLaser()
    {
        isActive = true;
        if (laserVisual) laserVisual.SetActive(true);
        GetComponent<Collider>().enabled = true;
        // Can’t be disabled again
    }
}