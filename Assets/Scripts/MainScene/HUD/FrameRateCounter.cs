using UnityEngine;
using UnityEngine.UI;




public class FrameRateCounter : MonoBehaviour
{
    private float m_TimeSinceUpdate = 0f;
    private string m_KeyName = "FPSCounter";
    private System.Text.StringBuilder m_StringBuilder = new System.Text.StringBuilder();
    private Text m_Text;




    private void Awake()
    {
        m_Text = GetComponent<Text>();
        if (!PlayerPrefs.HasKey(m_KeyName))
        {
            PlayerPrefs.SetInt(m_KeyName, 0);
            m_Text.text = "";
        }
    }



    private void Update()
    {
        if (PlayerPrefs.GetInt(m_KeyName) == 0)
        {
            if (m_Text.text != "")
                m_Text.text = "";
            return;
        }

        m_TimeSinceUpdate += Time.unscaledDeltaTime;

        if (m_TimeSinceUpdate > 0.5f)
        {
            m_TimeSinceUpdate = 0f;

            int fps = (int)(1f / Time.unscaledDeltaTime);
            double frameTime = System.Math.Round(Time.unscaledDeltaTime * 1000.0, 2);

            m_Text.text = m_StringBuilder.Clear().Append("FPS: ").AppendLine(fps.ToString()).Append("Frametime: ").Append(frameTime).Append(" ms").ToString();
        }
    }
}
