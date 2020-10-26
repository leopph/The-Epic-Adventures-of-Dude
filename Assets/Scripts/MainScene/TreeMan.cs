using UnityEngine;


public class TreeMan : MonoBehaviour
{
    private bool m_FacingLeft = true;
    private Canvas m_TextBoxCanvas;
    private RectTransform m_TextBoxRectTransform;

    public Transform m_Target;


    void Start()
    {
        m_TextBoxCanvas = GetComponentInChildren<Canvas>();
        m_TextBoxRectTransform = GetComponentInChildren<RectTransform>();

        if (!m_FacingLeft)
            flip();
    }


    // Update is called once per frame
    void Update()
    {
        if ((m_Target.position.x > transform.position.x && m_FacingLeft) || (m_Target.position.x < transform.position.x && !m_FacingLeft))
            flip();

        if (Vector2.Distance(m_Target.position, transform.position) > 5 && m_TextBoxCanvas.enabled)
            m_TextBoxCanvas.enabled = false;
        else if (Vector2.Distance(m_Target.position, transform.position) <= 5 && !m_TextBoxCanvas.enabled)
            m_TextBoxCanvas.enabled = true;
    }


    private void flip()
    {
        Vector3 tmp = m_TextBoxRectTransform.position;
        m_FacingLeft = !m_FacingLeft;

        transform.localScale = Vector3.Reflect(transform.localScale, Vector3.left);
        m_TextBoxRectTransform.localScale = Vector3.Reflect(m_TextBoxRectTransform.localScale, Vector3.left);
        m_TextBoxRectTransform.position = tmp;
    }
}
