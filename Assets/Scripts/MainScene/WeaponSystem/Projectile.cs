using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]


public abstract class Projectile : MonoBehaviour
{
    public float m_LifeTime = 69f;
    protected float m_TimeLeft = 0f;

    protected float m_MinDMG;
    protected float m_MaxDMG;

    protected Rigidbody2D m_Rigidbody;




    protected virtual void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
    }



    public float Damage()
    {
        return (m_MaxDMG - m_MinDMG) * Random.value + m_MinDMG;
    }



    public void Refresh()
    {
        m_TimeLeft = m_LifeTime;
        m_Rigidbody.simulated = true;
        transform.parent = null;
        transform.DetachChildren();
        gameObject.SetActive(false);
    }
}
