using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButtonHandler : MonoBehaviour
{
    // Method to load the main menu scene
    public void GoToHomePage()
    {
        SceneManager.LoadScene("HomePage");
    }
}
