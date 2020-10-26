using UnityEngine;


public class MainMenuVSyncButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponentInChildren<UnityEngine.UI.Text>().text = "VSync: " + (QualitySettings.vSyncCount == 1 ? "On" : QualitySettings.vSyncCount == 0 ? "Off" : "ERROR");
    }

    // Called by the vsync button to change vsync state and update button text
    public void ChangeVSync()
    {
        if (QualitySettings.vSyncCount == 0)
        {
            QualitySettings.vSyncCount = 1;
            GetComponentInChildren<UnityEngine.UI.Text>().text = "VSync: On";
        }
        else if (QualitySettings.vSyncCount == 1)
        {
            QualitySettings.vSyncCount = 0;
            GetComponentInChildren<UnityEngine.UI.Text>().text = "VSync: Off";
        }
    }
}
