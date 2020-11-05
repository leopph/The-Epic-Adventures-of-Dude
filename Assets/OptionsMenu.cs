using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    TextMeshProUGUI m_VSyncText;
    Slider m_VolumeSlider;


    private void Start()
    {
        m_VSyncText = GameObject.Find("VSyncButton").GetComponentInChildren<TextMeshProUGUI>();
        m_VSyncText.text = "VSYNC: " + (QualitySettings.vSyncCount == 1 ? "ON" : QualitySettings.vSyncCount == 0 ? "OFF" : "ERROR");

        m_VolumeSlider = GameObject.Find("VolumeSlider").GetComponent<Slider>();
        m_VolumeSlider.value = AudioListener.volume;
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
}
