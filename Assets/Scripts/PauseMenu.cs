using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;




public class PauseMenu : MonoBehaviour
{
    private static AudioManager m_AudioManager;
    private static CheckpointManager m_CheckPointManager;
    private static bool m_Paused = false;
    public static bool IsPaused => m_Paused;

    private Canvas m_Canvas;
    private Text m_VSyncButtonText;
    private Text m_FPSCounterButtonText;




    private void Awake()
    {
        m_Canvas = GetComponent<Canvas>();
        m_Canvas.enabled = false;
    }



    private void Start()
    {
        m_AudioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        m_CheckPointManager = GameObject.Find("CheckpointManager").GetComponent<CheckpointManager>();
        m_VSyncButtonText = transform.Find("Options").Find("VSyncButton").GetComponentInChildren<Text>();
        m_FPSCounterButtonText = transform.Find("Options").Find("FPSCounterButton").GetComponentInChildren<Text>();

        m_VSyncButtonText.text = "VSYNC: " + (QualitySettings.vSyncCount == 0 ? "OFF" : QualitySettings.vSyncCount == 1 ? "ON" : "ERROR");
        m_FPSCounterButtonText.text = "FPS COUNTER: " + (PlayerPrefs.GetInt("FPSCounter") == 1 ? "ON" : "OFF");
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

        transform.Find("Main").gameObject.SetActive(true);
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
        for (int i = 1; i < transform.childCount; i++)
            transform.GetChild(i).gameObject.SetActive(false);
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



    public void ChangeVsync()
    {
        switch (QualitySettings.vSyncCount)
        {
            case 0:
                QualitySettings.vSyncCount = 1;
                break;
            case 1:
                QualitySettings.vSyncCount = 0;
                break;
            default:
                QualitySettings.vSyncCount = 1;
                break;
        }

        m_VSyncButtonText.text = "VSYNC: " + (QualitySettings.vSyncCount == 0 ? "OFF" :  "ON");
    }



    public void SwitchFPSCounter()
    {
        PlayerPrefs.SetInt("FPSCounter", 1 - PlayerPrefs.GetInt("FPSCounter"));
        m_FPSCounterButtonText.text = "FPS COUNTER: " + (PlayerPrefs.GetInt("FPSCounter") == 1 ? "ON" : "OFF");
    }
}
