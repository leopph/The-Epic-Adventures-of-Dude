using UnityEngine;


public class MainMenuVSyncButton : MonoBehaviour
{
    private UnityEngine.UI.Text m_Text;


    // Start is called before the first frame update
    void Start()
    {
        m_Text = GetComponentInChildren<UnityEngine.UI.Text>();
        m_Text.text = "VSync: " + (QualitySettings.vSyncCount == 1 ? "On" : QualitySettings.vSyncCount == 0 ? "Off" : "ERROR");
    }


    // Called by the vsync button to change vsync state and update button text
    public void ChangeVSync()
    {
        if (QualitySettings.vSyncCount == 0)
            QualitySettings.vSyncCount = 1;

        else if (QualitySettings.vSyncCount == 1)
            QualitySettings.vSyncCount = 0;

        m_Text.text = "VSync: " + (QualitySettings.vSyncCount == 1 ? "On" : QualitySettings.vSyncCount == 0 ? "Off" : "ERROR");
    }
}
