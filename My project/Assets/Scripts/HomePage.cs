using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomePage : MonoBehaviour
{

    public void GoToPlayScene()
    {
        SceneManager.LoadScene("PlayScene");
    }
    // Load the scene associated with gameplay
    public void GoToLoaderPageScene()
    {
        SceneManager.LoadScene("LoaderPage");
    }

    // Load the scene associated with credits
    public void GoToCreditsScene()
    {
        SceneManager.LoadScene("CreditsScene");
    }

    // Load the scene associated with settings
    public void GoToSettingsScene()
    {
        SceneManager.LoadScene("Settings");
    }
}

