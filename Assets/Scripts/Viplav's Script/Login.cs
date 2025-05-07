using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//ng Photon.Pun;

public class Login : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public InputField nameField;
    public InputField passwordField;
    public Button SubmitButton;

    
    public void CallLogin()
    {
        StartCoroutine((LoggedIn()));
    }
    IEnumerator LoggedIn()
    {
        WWWForm form = new WWWForm();
        form.AddField("name", nameField.text);
        form.AddField("password", passwordField.text);
        // Creates A POST request that sends the form data (username and password) to "https://localhost/sqlconnect/register.php"

        UnityWebRequest www = UnityWebRequest.Post("http://localhost:8888/sqlconnect/login.php", form);
        {
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("network error" + www.error);
            }
            else
            {
                string responseText = www.downloadHandler.text.Trim();
                Debug.Log("response text" + responseText);

                // if there are 0 errors then Load Scene Main Menu
                if (responseText[0] == '0')
                {
                    DBManager.username = nameField.text;
                    // converts the score to an integer and stores it in the score variable
                    DBManager.score = int.Parse(responseText.Split('\t')[1]);
                    SceneManager.LoadScene("Lobby");

                }
                else
                {
                   Debug.LogError("login failed. Error #" + responseText);
                }
            }
        }
    }
    public void VerifyInputs()
    {
        // the Submit button will only work if the name Field and Password field length is more than or equal to 8 the submit button is reference to the Log In button
        SubmitButton.interactable = (nameField.text.Length >= 8 && passwordField.text.Length >= 8);
    }
}
