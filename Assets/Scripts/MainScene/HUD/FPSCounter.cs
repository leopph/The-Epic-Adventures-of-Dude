using UnityEngine;
using System.Collections;
using TMPro;




public class FPSCounter : MonoBehaviour
{
    private string m_KeyName = "FPSCounter";
    private System.Text.StringBuilder m_StringBuilder = new System.Text.StringBuilder();

    private TextMeshProUGUI m_Text;
    private int m_FPS;
    private double m_FrameTime;




    private void Awake()
    {
        m_Text = GetComponent<TextMeshProUGUI>();
        if (!PlayerPrefs.HasKey(m_KeyName))
        {
            PlayerPrefs.SetInt(m_KeyName, 0);
            m_Text.text = "";
        }
    }



    private IEnumerator Start()
    {
        while (true)
        {
            m_FPS = Mathf.RoundToInt(1 / Time.unscaledDeltaTime);
            m_FrameTime = System.Math.Round(1000 * Time.unscaledDeltaTime, 2);
            yield return new WaitForSecondsRealtime(0.5f);
        }    
    }



    private void OnGUI()
    {
        if (PlayerPrefs.GetInt(m_KeyName) == 1)
            m_Text.text = m_StringBuilder.Clear().AppendLine(SystemInfo.graphicsDeviceType.ToString()).Append(m_FPS).AppendLine(" FPS").Append(m_FrameTime).Append(" ms").ToString();
        else
            m_Text.text = "";
    }
}
