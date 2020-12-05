using UnityEngine;




public class CameraShake : MonoBehaviour
{
    private float m_Offset = 0.2f;
    private float m_Time = 0.1f;


    public void Shake()
    {
        InvokeRepeating("DoShake", 0, 0.01f);
        Invoke("StopShake", m_Time);
    }


    private void DoShake() { transform.localPosition += new Vector3(Random.Range(-m_Offset, m_Offset), Random.Range(-m_Offset, m_Offset), transform.localPosition.z); }
    private void StopShake()
    {
        CancelInvoke("DoShake");
        transform.localPosition = Vector3.zero;
    }
}
