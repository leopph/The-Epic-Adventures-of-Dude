using UnityEngine;


public class MainMenu : MonoBehaviour
{
    private AudioManager m_AudioManager;




    private void Awake()
    {
        GameObject.Find("VersionNumber").GetComponent<UnityEngine.UI.Text>().text = Application.version;
        m_AudioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }



    private void Start()
    {
        m_AudioManager.PlaySound("MainMenuTheme");
    }



    public void OnClick()
    {
        m_AudioManager.PlaySound("MainMenuButton");
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
