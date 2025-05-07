// using UnityEngine;
// using System.Collections;

// public class EscapeRoute : MonoBehaviour
// {
//     Target tar;
//     public ManagerPassword BankMapPressed;
//     public TakingGold GoldCounter;
//     public GameObject escapeRoute;
    
//     // Start is called once before the first execution of Update after the MonoBehaviour is created
//     void Start()
//     {
//         tar = escapeRoute.GetComponent<Target>();
//         BankMapPressed  = FindObjectOfType<ManagerPassword>();
//         GoldCounter  = FindObjectOfType<TakingGold>();
//         tar.enabled = false;
//     }

//     // Update is called once per frame
//     void Update()
//     {
        
//        StartCoroutine(RevealBankMap());

//     }
//     IEnumerator RevealBankMap()
//     {
//         if (GoldCounter.goldAmount >= 0 && BankMapPressed.count > 1)
//         {
//             yield return new WaitForSeconds(1.5f);
//             tar.enabled = true;
//             Debug.Log("Escape Route Unlocked");
//             tar.enabled = true;
            
//         }
//     }
// }

