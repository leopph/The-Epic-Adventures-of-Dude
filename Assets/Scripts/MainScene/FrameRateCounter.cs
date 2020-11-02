using UnityEngine;
using UnityEngine.UI;


public class FrameRateCounter : MonoBehaviour
{
    private Text m_Text = null;
    private System.Text.StringBuilder m_StringBuilder;
    private float m_TimeSinceUpdate = 0f;


    void Start()
    {
        m_Text = GetComponent<Text>();
        m_StringBuilder = new System.Text.StringBuilder();

        GetComponent<RectTransform>().anchoredPosition = new Vector2(0, Screen.height - GetComponent<RectTransform>().rect.height);
    }


    void Update()
    {
        m_TimeSinceUpdate += Time.unscaledDeltaTime;

        if (m_TimeSinceUpdate > 0.5f)
        {
            m_TimeSinceUpdate = 0f;

            int FPS = (int)(1f / Time.unscaledDeltaTime);
            double frameTime = System.Math.Round(Time.unscaledDeltaTime * 1000.0, 2);

            m_Text.text = m_StringBuilder.Clear().Append("FPS: ").AppendLine(FPS.ToString()).Append("Frametime: ").Append(frameTime).Append(" ms").ToString();
        }
    }
}
