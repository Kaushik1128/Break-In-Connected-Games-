using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class WebTest : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    IEnumerator Start()
    {
        // this is another php file we will create 
        UnityWebRequest request = UnityWebRequest.Get("http://localhost:8888/sqlconnect/webtest.php");
        // "request" will be returned with information from the web which will be later used 
        yield return request.SendWebRequest();
        // will print out whatever is printed from the php 
        Debug.Log(request.downloadHandler.text);
    }

    // Update is called once per frame
    
}
