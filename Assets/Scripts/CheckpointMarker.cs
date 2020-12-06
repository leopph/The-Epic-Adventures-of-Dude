using UnityEngine;
using TMPro;


public class CheckpointMarker : MonoBehaviour
{
    private TextMeshProUGUI m_Text;
    private float m_TimeLeft = 0;


    private void Awake()
    {
        m_Text = GetComponent<TextMeshProUGUI>();
        m_Text.text = "";
    }


    private void Update()
    {
        if (m_Text.text != "")
        {
            m_TimeLeft -= Time.deltaTime;

            if (m_TimeLeft <= 0)
                m_Text.text = "";
        }
    }


    public void Display()
    {
        m_Text.text = "Checkpoint reached.";
        m_TimeLeft = 2f;
    }
}
