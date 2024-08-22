using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButtonHandler : MonoBehaviour
{
    // Method to load the main menu scene
    public void GoToHomePage()
    {
        SceneManager.LoadScene("HomePage");
    }
    public void GoToBackPage()
    {
        SceneManager.LoadScene("NewLoadPage");
    }
    public void GoToPlayScene()
    {
        SceneManager.LoadScene("NewPlayScene");
    }
    public void GoToCalculateScene()
    {
        SceneManager.LoadScene("CalculationScene");
    }
}
