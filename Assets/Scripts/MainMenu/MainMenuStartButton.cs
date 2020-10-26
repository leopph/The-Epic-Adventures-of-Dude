using UnityEngine;


public class MainMenuStartButton : MonoBehaviour
{
    // Called by the start button to load the first scene
    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
}
