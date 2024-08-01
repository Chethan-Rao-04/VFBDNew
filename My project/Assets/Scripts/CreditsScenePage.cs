using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsPage : MonoBehaviour
{
    // Method to load the main menu scene
    public void GoToHomePage()
    {
        SceneManager.LoadScene("HomePage"); // Ensure "MainMenu" is the name of your main menu scene
    }
}


