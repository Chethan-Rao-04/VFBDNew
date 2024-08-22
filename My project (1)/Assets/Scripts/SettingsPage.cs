using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsPage : MonoBehaviour
{
    // Method to load the settings scene
    public void LoadSettingsScene()
    {
        SceneManager.LoadScene("SettingsScene");
    }

    // Method to go back to the main menu
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}

