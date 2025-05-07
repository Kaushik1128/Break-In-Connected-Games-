using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//ing Photon.Pun;

public class MainMenu : MonoBehaviour
{
    public Text welcomeText;
    private void Start()
    {
        
        // if the user is logged in then load the main menu
        if (DBManager.LoggedIn)
        {
            welcomeText.text = "Welcome, " + DBManager.username;
            // loads the scene Main Menu
            //otonNetwork.LoadLevel("MainMenu");
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void GoToRegister()
    {
        SceneManager.LoadScene("Register");
    }
    public void GoToLogin()
    {
        SceneManager.LoadScene("LoginMenu");
    }
   
}
