using UnityEngine;
using UnityEngine.UI;


public class FrameRateCounter : MonoBehaviour
{
    private Text m_Text = null;
    private float m_TimeSinceUpdate = 0f;


    void Start()
    {
        m_Text = GetComponent<Text>();
        GetComponent<RectTransform>().anchoredPosition = new Vector2(0, Screen.height - GetComponent<RectTransform>().rect.height);
    }


    void Update()
    {
        m_TimeSinceUpdate += Time.unscaledDeltaTime;

        if (m_TimeSinceUpdate > 0.5f)
        {
            m_Text.text = ("FPS: " + (int)(1000.0f / (Time.unscaledDeltaTime * 1000.0f)) + System.Environment.NewLine
            + "Frametime: " + System.Math.Round(Time.unscaledDeltaTime * 1000f, 2) + " ms");

            m_TimeSinceUpdate = 0f;
        }
    }
}
