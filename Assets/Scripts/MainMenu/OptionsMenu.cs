using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class OptionsMenu : MonoBehaviour
{
    private Slider m_VolumeSlider;
    private TextMeshProUGUI m_VSyncText;
    private TextMeshProUGUI m_FPSText;


    private void Start()
    {
        m_VolumeSlider = GameObject.Find("VolumeSlider").GetComponent<Slider>();
        m_VolumeSlider.value = AudioListener.volume;

        m_VSyncText = GameObject.Find("VSyncButton").GetComponentInChildren<TextMeshProUGUI>();
        m_VSyncText.text = "VSYNC: " + (QualitySettings.vSyncCount == 1 ? "ON" : QualitySettings.vSyncCount == 0 ? "OFF" : "ERROR");

        m_FPSText = GameObject.Find("FPSButton").GetComponentInChildren<TextMeshProUGUI>();
        m_FPSText.text = "FRAMERATE COUNTER: " + (PlayerPrefs.HasKey("FPSCounter") && PlayerPrefs.GetInt("FPSCounter") == 1 ? "ON" : "OFF");

        gameObject.SetActive(false);
    }


    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
    }


    public void SetVSync()
    {
        if (QualitySettings.vSyncCount == 0)
            QualitySettings.vSyncCount = 1;

        else if (QualitySettings.vSyncCount == 1)
            QualitySettings.vSyncCount = 0;

        m_VSyncText.text = "VSYNC: " + (QualitySettings.vSyncCount == 1 ? "ON" : QualitySettings.vSyncCount == 0 ? "OFF" : "ERROR");
    }


    public void SetFPSCounter()
    {
        if (!PlayerPrefs.HasKey("FPSCounter"))
            PlayerPrefs.SetInt("FPSCounter", 0);

        PlayerPrefs.SetInt("FPSCounter", 1 - PlayerPrefs.GetInt("FPSCounter"));
        m_FPSText.text = "FRAMERATE COUNTER: " + (PlayerPrefs.GetInt("FPSCounter") == 1 ? "ON" : "OFF");
    }
}
