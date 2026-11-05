using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void StartGame()
    {
        // Load the main game scene
        SceneManager.LoadScene("SampleScene"); // replace with your game scene name
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
