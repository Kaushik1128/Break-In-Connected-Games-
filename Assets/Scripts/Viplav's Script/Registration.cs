using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.Networking;
//ing Photon.Pun;

public class Registration : MonoBehaviour
{
    public InputField nameField;
    public InputField passwordField;
    public Button SubmitButton;

    
    public void CallRegister()
    {
        StartCoroutine(Register());
    }
    public void VerifyInputs()
    {
        // the Submit button will only work if the name Field and Password field length is more than or equal to 8 the submit button is reference to the Log In button
        SubmitButton.interactable = (nameField.text.Length >= 8 && passwordField.text.Length >= 8);
    }
    IEnumerator Register()
    {
        // Used for Authentication Login
        WWWForm form = new WWWForm();
        form.AddField("name", nameField.text);
        form.AddField("password", passwordField.text);
        // Creates A POST request that sends the form data (username and password) to "https://localhost/sqlconnect/register.php"
        //WWW www = new WWW("http://localhost/sqlconnect/register.php", form);
        UnityWebRequest www = UnityWebRequest.Post("http://localhost:8888/sqlconnect/register.php", form);
        {
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log("network error" + www.error);
            }
            else
            {
                string responseText = www.downloadHandler.text.Trim();
                Debug.Log("response text" + responseText);

                // if there are 0 errors then Load Scene Main Menu
                if (responseText == "0")
                {
                    Debug.Log("User Succesfully Created");
                    SceneManager.LoadScene("MainMenu");

                }
                else
                {
                    Debug.Log("User Creation Failed Error #" + responseText);
                }
            }

                // Start is called once before the first execution of Update after the MonoBehaviour is created

            }
        }
    }



