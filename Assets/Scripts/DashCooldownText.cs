using UnityEngine;
using UnityEngine.UI;




public class DashCooldownText : MonoBehaviour
{
    private Text m_Text;

    public float m_DisplayTime;
    private float m_RemainingTime;


    private void Awake()
    {
        m_Text = GetComponent<Text>();
    }


    private void OnEnable()
    {
        m_RemainingTime = m_DisplayTime;
    }


    private void Update()
    {
        m_RemainingTime -= Time.deltaTime;

        if (m_RemainingTime <= 0f)
            gameObject.SetActive(false);
    }


    public void Set(int cooldown)
    {
        m_Text.text = "Cooldown: " + cooldown;
        m_RemainingTime = m_DisplayTime;
        gameObject.SetActive(true);
    }
}
