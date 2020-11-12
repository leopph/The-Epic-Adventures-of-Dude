using UnityEngine;


public class MainMenu : MonoBehaviour
{
    private void Awake()
    {
        GameObject.Find("VersionNumber").GetComponent<UnityEngine.UI.Text>().text = Application.version;
    }



    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }


    
    public void QuitGame()
    {
        Application.Quit();
    }
}
