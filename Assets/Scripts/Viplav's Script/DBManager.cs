using UnityEngine;

public static class DBManager 
{
    public static string username;
    public static int score;
    // checks if the user is logged in
    public static bool LoggedIn { get { return username != null; } }
    public static void LogOut()
    {
        // sets the username to null
        username = null;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created

}
