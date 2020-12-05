using UnityEngine;
using UnityEngine.SceneManagement;




public class PauseMenu : MonoBehaviour
{
    private static AudioManager m_AudioManager;
    private static CheckpointManager m_CheckPointManager;
    private static bool m_Paused = false;

    private Canvas m_Canvas;

    public static bool IsPaused => m_Paused;


    private void Awake()
    {
        m_Canvas = GetComponent<Canvas>();
        m_Canvas.enabled = false;
    }


    private void Start()
    {
        m_AudioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        m_CheckPointManager = GameObject.Find("CheckpointManager").GetComponent<CheckpointManager>();
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            if (m_Paused)
                Resume();
            else
                Pause();
    }


    private void Pause()
    {
        if (m_Paused)
            return;

        m_Paused = true;
        Time.timeScale = 0f;
        m_AudioManager.PauseSounds();
        m_AudioManager.PauseMusic("Ambient");
        m_AudioManager.PlayMusic("Pause");
        m_Canvas.enabled = true;
    }


    public void Resume()
    {
        if (!m_Paused)
            return;

        m_Paused = false;
        Time.timeScale = 1f;
        m_AudioManager.StopMusic("Pause");
        m_AudioManager.ResumeMusic("Ambient");
        m_AudioManager.ResumeSounds();
        m_Canvas.enabled = false;
    }


    public void ReloadLastCheckpoint()
    {
        Resume();
        m_CheckPointManager.ReloadCheckPoint();
    }

    public void BackToMenu()
    {
        Time.timeScale = 1f;
        m_Paused = false;
        SceneManager.LoadScene(0);
    }

    public void ExitGame() { Application.Quit(); }
}
