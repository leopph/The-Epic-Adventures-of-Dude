using UnityEngine;
using UnityEngine.UI;




public class FrameRateCounter : MonoBehaviour
{
    private float m_TimeSinceUpdate = 0f;
    private int m_FrameCount = 0;
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
        m_FrameCount++;

        if (m_TimeSinceUpdate > 0.5f)
        {
            int fps = Mathf.RoundToInt(m_FrameCount / m_TimeSinceUpdate);
            double frameTime = System.Math.Round(1000 * m_TimeSinceUpdate / m_FrameCount, 2);

            m_Text.text = m_StringBuilder.Clear().AppendLine(SystemInfo.graphicsDeviceType.ToString()).Append(fps).AppendLine(" FPS").Append(frameTime).Append(" ms").ToString();

            m_TimeSinceUpdate = 0f;
            m_FrameCount = 0;
        }
    }
}
