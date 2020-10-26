using UnityEngine;
using UnityEngine.UI;


public class FrameRateCounter : MonoBehaviour
{
    private Text m_Text = null;


    void Start()
    {
        m_Text = GetComponent<Text>();
        GetComponent<RectTransform>().anchoredPosition = new Vector2(0, Screen.height - GetComponent<RectTransform>().rect.height);
    }


    void OnGUI()
    {
        m_Text.text = ("FPS: " + (int)(1000.0f / (Time.unscaledDeltaTime * 1000.0f)) + System.Environment.NewLine
        + "Frametime: " + System.Math.Round(Time.unscaledDeltaTime * 1000f, 1) + " ms");
    }
}
