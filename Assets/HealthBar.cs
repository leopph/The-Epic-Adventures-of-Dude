using UnityEngine;
using UnityEngine.UI;




public class HealthBar : MonoBehaviour
{
    private Slider m_Slider;




    private void Awake()
    {
        m_Slider = GetComponent<Slider>();
    }



    public void Init(float maxValue)
    {
        m_Slider.maxValue = maxValue;
        m_Slider.value = maxValue;
    }



    public void Set(float value)
    {
        m_Slider.value = value;
    }
}
