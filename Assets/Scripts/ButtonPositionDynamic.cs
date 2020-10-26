using UnityEngine;


public class ButtonPositionDynamic : MonoBehaviour
{
    public float m_X = 0.5f;
    public float m_Y = 0.5f;


    // Start is called before the first frame update
    void Start()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.anchorMin = Vector2.zero;
        rectTransform.anchorMax = Vector2.zero;
        rectTransform.pivot = Vector2.zero;
        rectTransform.anchoredPosition = new Vector2(Screen.width * m_X - rectTransform.rect.width / 2f, Screen.height * m_Y - rectTransform.rect.height / 2f);
    }
}
