using UnityEngine;


public class MainMenuButtonManager : MonoBehaviour
{
    public RectTransform[] m_Buttons;

    
    // Start is called before the first frame update
    void Start()
    {
        m_Buttons[0].anchorMin = Vector2.zero;
        m_Buttons[0].anchorMax = Vector2.zero;
        m_Buttons[0].pivot = Vector2.zero;

        m_Buttons[0].anchoredPosition = new Vector2(Screen.width / 2f - m_Buttons[0].rect.width / 2f, Screen.height / 2f - m_Buttons[0].rect.height / 2f);

        for (int i = 1; i < m_Buttons.Length; i++)
        {
            m_Buttons[i].anchorMin = Vector2.zero;
            m_Buttons[i].anchorMax = Vector2.zero;
            m_Buttons[i].pivot = Vector2.zero;

            m_Buttons[i].anchoredPosition = new Vector2(m_Buttons[0].anchoredPosition.x, m_Buttons[i - 1].anchoredPosition.y - m_Buttons[i - 1].rect.height / 2f - m_Buttons[i].rect.height / 2f);
        }
    }

    // Update GUI in case the window gets transformed
    void OnGUI()
    {
        m_Buttons[0].anchoredPosition = new Vector2(Screen.width / 2f - m_Buttons[0].rect.width / 2f, Screen.height / 2f - m_Buttons[0].rect.height / 2f);

        for (int i = 1; i < m_Buttons.Length; i++)
            m_Buttons[i].anchoredPosition = new Vector2(m_Buttons[0].anchoredPosition.x, m_Buttons[i - 1].anchoredPosition.y - m_Buttons[i - 1].rect.height / 2f - m_Buttons[i].rect.height / 2f);
    }
}
