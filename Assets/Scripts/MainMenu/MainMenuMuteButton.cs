using UnityEngine;


public class MainMenuMuteButton : MonoBehaviour
{
    private UnityEngine.UI.Text m_Text;


    // Start is called before the first frame update
    void Start()
    {
        m_Text = GetComponentInChildren<UnityEngine.UI.Text>();
        m_Text.text = AudioListener.volume == 1f ? "Mute" : AudioListener.volume == 0f ? "Unmute" : "Audio ERROR";
    }

    
    // Called by the mute button to change the audio state and button text
    public void ChangeAudioState()
    {
        if (AudioListener.volume == 1f)
            AudioListener.volume = 0f;

        else if (AudioListener.volume == 0f)
            AudioListener.volume = 1f;

        m_Text.text = AudioListener.volume == 1f ? "Mute" : AudioListener.volume == 0f ? "Unmute" : "Audio ERROR";
    }
}
