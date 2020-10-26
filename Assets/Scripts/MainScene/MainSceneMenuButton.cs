using UnityEngine;


public class MainSceneMenuButton : MonoBehaviour
{
    RectTransform m_RectTransform = null;


    // Start is called before the first frame update
    void Start()
    {
        m_RectTransform = GetComponent<RectTransform>();

        m_RectTransform.anchorMin = Vector2.zero;
        m_RectTransform.anchorMax = Vector2.zero;
        m_RectTransform.pivot = Vector2.zero;

        m_RectTransform.anchoredPosition = new Vector2(Screen.width - m_RectTransform.rect.width, Screen.height - m_RectTransform.rect.height);
    }

    // Update button placement in case of window resize
    void OnGUI()
    {
        m_RectTransform.anchoredPosition = new Vector2(Screen.width - m_RectTransform.rect.width, Screen.height - m_RectTransform.rect.height);
    }

    // On click go back to main menu
    public void BackToMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
