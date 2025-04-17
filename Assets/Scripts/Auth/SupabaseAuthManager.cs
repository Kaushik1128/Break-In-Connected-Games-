using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

public class SupabaseAuthManager : MonoBehaviour
{
    [Header("Supabase Settings")]
    public string supabaseURL = "https://ubphgtmsugzliqeciaxg.supabase.co";
    public string apiKey = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6InVicGhndG1zdWd6bGlxZWNpYXhnIiwicm9sZSI6ImFub24iLCJpYXQiOjE3NDQ4MDE0NDcsImV4cCI6MjA2MDM3NzQ0N30.W7F9CIdKSQ00KDm5ilE9iI9IYk5H-qy40g74nD4WnUg";

    [Header("UI References")]
    public TMP_InputField emailInput;
    public TMP_InputField passwordInput;
    public TMP_Text feedbackText;

    public void OnLoginButton()
    {
    StartCoroutine(LoginUser(emailInput.text, passwordInput.text));
    }
    public void OnRegisterButton()
    {
    StartCoroutine(RegisterUser(emailInput.text, passwordInput.text));
    }
    IEnumerator LoginUser(string email, string password)
    {
        string json = $"{{\"email\":\"{email}\",\"password\":\"{password}\"}}";
        UnityWebRequest request = new UnityWebRequest($"{supabaseURL}/auth/v1/token?grant_type=password", "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("apikey", apiKey);
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            ShowMessage("Login successful!", false);
            yield return new WaitForSeconds(1.5f);
            SceneManager.LoadScene("Lobby Scene");
        }
        else
        {
            string error = request.downloadHandler.text;
            if (error.Contains("Invalid login credentials"))
                ShowMessage("Incorrect email or password.", true);
            else
                ShowMessage("Login error: " + error, true);
        }
    }

    IEnumerator RegisterUser(string email, string password)
    {
        string json = $"{{\"email\":\"{email}\",\"password\":\"{password}\"}}";
        UnityWebRequest request = new UnityWebRequest($"{supabaseURL}/auth/v1/signup", "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("apikey", apiKey);
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            ShowMessage("Registration successful! You can now log in.", false);
        }
        else
        {
            string error = request.downloadHandler.text;
            if (error.Contains("User already registered"))
                ShowMessage("Email is already registered.", true);
            else
                ShowMessage("Registration error: " + error, true);
        }
    }

    void ShowMessage(string message, bool isError)
    {
        feedbackText.text = message;
        feedbackText.color = isError ? Color.red : Color.green;
    }
}
